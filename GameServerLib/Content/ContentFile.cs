
using System;
using System.IO;
using System.Globalization;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using INIParser;
using Chronobreak.GameServer.Logging;
using log4net;

namespace Chronobreak.GameServer.Content
{
    public class INIContentFile
    {
        private static readonly ILog _logger = LoggerProvider.GetLogger();
        public IniFile? INI { get; init; }
        public string[] Sections => INI?.Sections ?? [];
        public string Name { get; init; }
        public INIContentFile()
        {
            Name = string.Empty;
        }
        public INIContentFile(string iniPath)
        {
            Name = Path.GetFileNameWithoutExtension(iniPath);
            if (!File.Exists(iniPath))
            {
                _logger.Warn($"File \"{iniPath}\" doesn't exist!");
                return;
            }
            INI = new(iniPath);
        }

        public string[] GetKeys(string section)
        {
            return INI?.GetKeys(section) ?? Array.Empty<string>();
        }

        public bool HasMentionOf(string section, string name)
        {
            return INI?[section, name] != null;
        }

        private void Warn(Exception e, string section, string name, int index = -1)
        {
            _logger.Warn($"Error at Section: {section}, Variable: {name}{((index != -1) ? ", Index: " + index : "")}, in file {Name}.ini");
            _logger.Warn(e.Message);
        }

        public string GetValue(string section, string name, string? defaultValue = default)
        {
            return GetString(section, name, defaultValue) ?? string.Empty;
        }

        public string GetString(string section, string name, string? defaultValue = default)
        {
            string? str = INI?[section, name];
            if (!string.IsNullOrEmpty(str))
            {
                return str.Trim('"');
            }
            return defaultValue ?? string.Empty;
        }

        public bool GetValue(string section, string name, bool defaultValue = default)
        {
            return GetBool(section, name, defaultValue);
        }
        public bool GetBool(string section, string name, bool defaultValue = default)
        {
            string? str = GetString(section, name);
            if (!string.IsNullOrEmpty(str))
            {
                str = str.ToLower();
                if (str is "true" or "1" or "yes")
                {
                    return true;
                }
                else if (str is "false" or "0" or "no")
                {
                    return false;
                }
                // bool.Parse exception message
                Exception e = new FormatException("String was not recognized as a valid Boolean");
                Warn(e, section, name);
            }
            return defaultValue;
        }

        public int GetValue(string section, string name, int defaultValue = default)
        {
            return GetInt(section, name, defaultValue);
        }
        public int GetInt(string section, string name, int defaultValue = default, int debugIndex = -1)
        {
            return (int)GetFloat(section, name, (float)defaultValue, debugIndex);
        }

        public float GetValue(string section, string name, float defaultValue = default)
        {
            return GetFloat(section, name, defaultValue);
        }

        public float GetFloat(string section, string name, string defaultSection, float defaultValue = 0)
        {
            //Improve this later
            return GetFloat(section, name, GetFloat(section, defaultSection, defaultValue));
        }

        public float GetFloat(string section, string name, float defaultValue = default, int debugIndex = -1)
        {
            string? str = GetString(section, name);
            if (!string.IsNullOrEmpty(str))
            {
                try
                {
                    string rStr = str;
                    //rStr = Regex.Replace(str, "[^0-9.+-]", string.Empty);
                    rStr = Regex.Replace(rStr, @"^-r", "-");
                    rStr = Regex.Replace(rStr, @"\.0000$", "");
                    rStr = Regex.Replace(rStr, @"[ewsqFr]+$", "");
                    string[] floatSectioned = rStr.Split('.');
                    string strToParse = "0";
                    string t = floatSectioned[0].Trim();
                    if (t != "")
                        strToParse = t;
                    if (floatSectioned.Length > 1)
                    {
                        t = floatSectioned[1].Trim();
                        if (t != "")
                            strToParse += "." + t;
                    }
                    return float.Parse(strToParse, NumberStyles.Any, CultureInfo.InvariantCulture);
                }
                catch (Exception e)
                {
                    Warn(e, section, name, debugIndex);
                }
            }
            return defaultValue;
        }

        public float[] GetFloatArray(string section, string name, float[] defaultValue)
        {
            var obj = GetString(section, name);
            if (obj != null)
            {
                var list = obj.Split(' ');
                if (defaultValue.Length == list.Length)
                {
                    for (var i = 0; i < defaultValue.Length; i++)
                    {
                        try
                        {
                            defaultValue[i] = float.Parse(list[i], NumberStyles.Any, CultureInfo.InvariantCulture);
                        }
                        catch (Exception e)
                        {
                            Warn(e, section, name, i);
                        }
                    }
                }
            }
            return defaultValue;
        }

        public int[] GetIntArray(string section, string name, int[] defaultValue)
        {
            var obj = GetString(section, name);
            if (obj != null)
            {
                var list = obj.Split(' ');
                if (defaultValue.Length == list.Length)
                {
                    for (var i = 0; i < defaultValue.Length; i++)
                    {
                        try
                        {
                            defaultValue[i] = (int)float.Parse(list[i], NumberStyles.Any, CultureInfo.InvariantCulture);
                        }
                        catch (Exception e)
                        {
                            Warn(e, section, name, i);
                        }
                    }
                }
            }
            return defaultValue;
        }

        public float[] GetMultiFloat(string section, string name, int num = 6, float defaultValue = 0, string affix = "")
        {
            var result = new float[num + 1];
            result[0] = affix != "" ?
                GetFloat(section, $"{name}0{affix}", defaultValue) :
                GetFloat(section, name, defaultValue);
            for (var i = 1; i < num + 1; i++)
            {
                result[i] = GetFloat(section, $"{name}{i}{affix}", result[0], i);
            }
            return result;
        }

        public int[] GetMultiInt(string section, string name, int num = 6, int defaultValue = 0)
        {
            var result = new int[num + 1];
            result[0] = GetInt(section, name, defaultValue);
            for (var i = 1; i < num + 1; i++)
            {
                result[i] = GetInt(section, $"{name}{i}", result[0], i);
            }
            return result;
        }

        public string?[] GetMultiString(string section, string name, string? defaultValue)
        {
            var result = new List<string?>();
            var first = GetString(section, name, null);
            if (first != null)
            {
                result.Add(first);
            }

            if (INI is not null)
            {
                foreach (var key in INI.GetKeys(section))
                {
                    if (key.StartsWith(name) && int.TryParse(key[name.Length..], out int i))
                    {
                        var str = GetString(section, key, null);
                        if (str != null)
                        {
                            while (result.Count <= i)
                            {
                                result.Add(defaultValue);
                            }
                            result[i] = str;
                        }
                    }
                }
            }

            return result.ToArray();
        }
    }
}
