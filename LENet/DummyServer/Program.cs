using LENet;
using System;

namespace DummyServer
{
    class Program
    {
        public class ENet
        {
            public Host host;
            public Peer peer;
            public string name;
            int iteration = 0;

            public int RunLoop(int timeout = 8)
            {
                int result;
                do
                {
                    result = RunOnce(timeout);
                } while (result > 0);
                return result;
            }

            public int RunOnce(int timeout = 8)
            {
                int result;
                Console.Out.WriteLine($"[{name}] Step: {iteration++}");
                var evnt = new Event();
                result = host.HostService(evnt, (uint)timeout);
                switch (evnt.Type)
                {
                    case EventType.CONNECT:
                        Console.WriteLine($"[{name}] Peer ({evnt.Peer.SessionID}) connected!");
                        peer = evnt.Peer;
                        break;
                    case EventType.DISCONNECT:
                        Console.WriteLine($"[{name}] Peer ({evnt.Peer.SessionID}) diconnected!");
                        break;
                    case EventType.RECEIVE:
                        Console.WriteLine($"[{name}] Peer ({evnt.Peer.SessionID}) sent data({evnt.Packet.DataLength})");

                        break;
                    default:
                        break;
                }
                if (result < 0)
                {
                    Console.Out.WriteLine($"[{name}] Error!");
                }
                return result;
            }
        }

        static void TestAsync()
        {
            var address = new Address("127.0.0.1", 5005);
            var server = new ENet { name = "Server" };
            var client = new ENet { name = "Client" };

            server.host = new Host(LENet.Version.Seasson8_Server, address, 32, 8, 0, 0);

            client.host = new Host(LENet.Version.Seasson8_Client, null, 1, 8, 0, 0);
            client.peer = client.host.Connect(address, 8);

            client.RunLoop();
            server.RunLoop();
            client.RunLoop();
            server.RunLoop();

            client.peer.Send(0, new Packet(42, PacketFlags.RELIABLE));

            client.RunLoop();
            server.RunLoop();

            client.peer.Send(0, new Packet(69, PacketFlags.RELIABLE));


            client.RunOnce();
            server.RunOnce();
        }

        static void Main(string[] args)
        {
            TestAsync();
        }
    }
}
