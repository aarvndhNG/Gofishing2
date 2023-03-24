using System;
using System.IO;
using Newtonsoft.Json;

namespace Gofishing
{
// Token: 0x02000002 RID: 2
internal class Config
{
// Token: 0x17000001 RID: 1
// (get) Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
// (set) Token: 0x06000002 RID: 2 RVA: 0x00002058 File Offset: 0x00000258
public catch rewards { get; set; } = new catch rewards[]
{
new catch reward()
};

// Token: 0x06000003 RID: 3 RVA: 0x00002064 File Offset: 0x00000264
public static Config GetConfig()
{
Config config = new Config();
bool flag = !File.Exists("tshock/catch configuration table.json");
if (flag)
{
using (StreamWriter streamWriter = new StreamWriter("tshock/catch configuration table.json"))
{
streamWriter.WriteLine(JsonConvert.SerializeObject(config, 1));
}
}
else
{
using (StreamReader streamReader = new StreamReader("tshock/catch configuration table.json"))
{
config = JsonConvert. DeserializeObject<Config>(streamReader. ReadToEnd());
}
}
return config;
}

// Token: 0x04000001 RID: 1
public int timed competition duration_minutes = 2;

// Token: 0x04000002 RID: 2
public int currency multiplier = 2;

// Token: 0x04000003 RID: 3
public string race time 1 = "08:00:00";

// Token: 0x04000004 RID: 4
public string race time 2 = "10:00:00";

// Token: 0x04000005 RID: 5
public string race time 3 = "12:00:00";

// Token: 0x04000006 RID: 6
public string race time 4 = "14:00:00";

// Token: 0x04000007 RID: 7
public string race time 5 = "16:00:00";

// Token: 0x04000008 RID: 8
public string race time 6 = "18:00:00";

// Token: 0x04000009 RID: 9
public string race time 7 = "20:00:00";

// Token: 0x0400000A RID: 10
public string race time 8 = "22:00:00";

// Token: 0x0400000C RID: 12
private const string path = "tshock/catch configuration table.json";
}
}

}
