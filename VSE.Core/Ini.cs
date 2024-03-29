using System.Collections.Generic;

namespace VSE.Core
{
    public class Ini
	{
		string path = "";

		public Ini(string IniPath)
		{ path = IniPath; }

		public void IniWriteValue(string section, string key, string value)
		{
			LXCSystem.Files.LXCIni.SetINI(this.path, section, key, value);
		}

		public void IniWriteConfig(string key, string value)
		{
			LXCSystem.Files.LXCIni.SetINI(this.path, "Config", key, value);
		}

		public string IniReadValue(string section, string key)
		{

			//StringBuilder stringBuilder = new StringBuilder(2048);
			string privateProfileString = LXCSystem.Files.LXCIni.GetINI(this.path, section, key, "");
			return privateProfileString;
		}

		public string IniReadConfig(string key)
		{

			string privateProfileString = LXCSystem.Files.LXCIni.GetINI(this.path, "Config", key, "");
			return privateProfileString;
		}

		public void IniDeleteKey(string section, string key)
		{
			if (section.Trim().Length > 0 && key.Trim().Length > 0)
			{
				LXCSystem.Files.LXCIni.SetINI(this.path, section, key, null);

			}
		}

		public void IniDeleteSection(string section)
		{
			LXCSystem.Files.LXCIni.SetINI(this.path, section, null, null);

		}

		public string[] IniReadAllKeys(string section)
		{
			List<string> s = LXCSystem.Files.LXCIni.GetSectionAllKey(this.path, section);
			string[] r = s.ToArray();
			return r;
		}



	}
}
