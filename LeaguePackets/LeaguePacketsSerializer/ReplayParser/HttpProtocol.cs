using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ENetUnpack.ReplayParser
{
    public abstract class HttpProtocolHandler
    {
        enum HttpState
        {
            Done,
            GetText,
            GetBinary,
            ContinueText,
            ContinueBinary,
        }

        private HttpState _httpState = HttpState.Done;
        private List<byte> _buffer = new List<byte>();
        private long _bufferExpectedLength = 0;

        public virtual void HandleTextPacket(string data, float time)
        {

        }

        public virtual void HandleBinaryPacket(byte[] data, float time)
        {

        }

        public void Read(byte[] data, float time)
        {
            switch(_httpState)
            {
                case HttpState.GetBinary:
                    HandleGetBinary(data, time);
                    break;
                case HttpState.GetText:
                    HandleGetText(data, time);
                    break;
                case HttpState.Done:
                    HandleDone(data, time);
                    break;
                case HttpState.ContinueBinary:
                    HandleContinueBinary(data, time);
                    break;
                case HttpState.ContinueText:
                    HandleContinueText(data, time);
                    break;
            }
        }

        private void HandleGetBinary(byte[] data, float time)
        {
            HandleBinaryPacket(data, time);
            _httpState = HttpState.Done;
        }

        private void HandleGetText(byte[] data, float time)
        {
            HandleTextPacket(Encoding.UTF8.GetString(data), time);
            _httpState = HttpState.Done;
        }

        private void HandleContinueBinary(byte[] data, float time)
        {
            _buffer.AddRange(data);
            if(_buffer.Count > _bufferExpectedLength)
            {
                throw new IOException("Buffer overrun!");
            }
            else if(_buffer.Count == _bufferExpectedLength)
            {
                HandleBinaryPacket(_buffer.ToArray(), time);
                _httpState = HttpState.Done;
                _buffer.Clear();
                _bufferExpectedLength = 0;
            }
        }

        private void HandleContinueText(byte[] data, float time)
        {
            _buffer.AddRange(data);
            if (_buffer.Count > _bufferExpectedLength)
            {
                throw new IOException("Buffer overrun!");
            }
            else if (_buffer.Count == _bufferExpectedLength)
            {
                HandleTextPacket(Encoding.UTF8.GetString(_buffer.ToArray()), time);
                _httpState = HttpState.Done;
                _buffer.Clear();
                _bufferExpectedLength = 0;
            }
        }


        private void HandleDone(byte[] data, float time)
        {
            if (data[0] == 'H' && data[1] == 'T' && data[2] == 'T' && data[3] == 'P')
            {
                HandleHttp(data, time);
            }
            else if (data[0] == 'G' && data[1] == 'E' && data[2] == 'T' && data[3] == ' ')
            {
                var requestText = Encoding.UTF8.GetString(data);
                if (requestText.Contains("\r\n"))
                {
                    _httpState = HttpState.Done;
                }
                else if (requestText.StartsWith("GET /observer-mode/rest/consumer/getGameDataChunk"))
                {
                    _httpState = HttpState.GetBinary;
                }
                else
                {
                    _httpState = HttpState.GetText;
                }
            }
            else if (data[0] == 'P' && data[1] == 'O' && data[2] == 'S' && data[3] == 'T' && data[4] == ' ')
            {
                var requestText = Encoding.UTF8.GetString(data);
                _httpState = HttpState.GetText;
            }
            // HEAD
            else if (data[0] == 'H' && data[1] == 'E' && data[2] == 'A' && data[3] == 'D' && data[4] == ' ')
            {

            }
            // OPTIONS
            else if (data[0] == 'O' && data[1] == 'P' && data[2] == 'T' && data[3] == 'I'
                && data[4] == 'O' && data[5] == 'N' && data[6] == 'S' && data[7] == ' ')
            {

            }
            else
            {
                throw new IOException("Bad chunk start!");
            }
        }

        private static readonly Regex RE_CONTENT_LEN = new Regex("Content-Length: ([0-9]+)", RegexOptions.IgnoreCase);

        private static byte[] HTTP_END = new byte[]{ 0x0D, 0x0A, 0x0D, 0x0A };

        private void HandleHttp(byte[] data, float time)
        {
            using (var stream = new MemoryStream(data))
            {
                int index = 0;
                int matchCount = 0;
                for(; index < data.Length && matchCount != 4; index ++)
                {
                    if(data[index] == HTTP_END[matchCount])
                    {
                        matchCount++;
                    }
                    else
                    {
                        matchCount = 0;
                    }
                }
                if(matchCount != 4)
                {
                    throw new IOException("Failed to find http end in stream!");
                }
                using (var binary = new BinaryReader(stream, Encoding.UTF8, true))
                {
                    var http = Encoding.UTF8.GetString(binary.ReadExactBytes(index));
                    var contentLengthMatch = RE_CONTENT_LEN.Match(http);
                    if(!contentLengthMatch.Success)
                    {
                        return;
                    }
                    var contentLength = long.Parse(contentLengthMatch.Groups[1].Value);
                    var content = binary.ReadExactBytes((int)binary.BytesLeft());
                    if (!http.Contains("application/octet-stream"))
                    {
                        if (content.Length < contentLength)
                        {
                            _buffer.AddRange(content);
                            _bufferExpectedLength = contentLength;
                            _httpState = HttpState.ContinueText;
                        }
                        else
                        {
                            HandleTextPacket(Encoding.UTF8.GetString(content), time);
                        }
                    }
                    else
                    {
                        if (content.Length < contentLength)
                        {
                            _buffer.AddRange(content);
                            _bufferExpectedLength = contentLength;
                            _httpState = HttpState.ContinueBinary;
                        }
                        else
                        { 
                            HandleBinaryPacket(content, time);
                        }
                    }
                }
            }
        }
    }
}
