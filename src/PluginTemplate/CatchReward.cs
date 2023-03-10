using System;

namespace Gofishing
{
	// Token: 0x02000003 RID: 3
	internal class 渔获奖励
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000005 RID: 5 RVA: 0x0000218E File Offset: 0x0000038E
		// (set) Token: 0x06000006 RID: 6 RVA: 0x00002196 File Offset: 0x00000396
		public string 鱼类ID作为奖励组 { get; set; } = "第一名";

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000007 RID: 7 RVA: 0x0000219F File Offset: 0x0000039F
		// (set) Token: 0x06000008 RID: 8 RVA: 0x000021A7 File Offset: 0x000003A7
		public Gofishing.Item1[] 包含物品 { get; set; } = new Gofishing.Item1[]
		{
			new Gofishing.Item1()
		};
	}
}
