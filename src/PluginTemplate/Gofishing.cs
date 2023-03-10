using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Threading.Tasks;
using Terraria;
using Terraria.Localization;
using TerrariaApi.Server;
using TShockAPI;
using TShockAPI.DB;
using TShockAPI.Models.PlayerUpdate;

namespace Gofishing
{
	// Token: 0x02000004 RID: 4
	[ApiVersion(2, 1)]
	public class Gofishing : TerrariaPlugin
	{
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000A RID: 10 RVA: 0x000021D8 File Offset: 0x000003D8
		public override string Author
		{
			get
			{
				return "大豆子";
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600000B RID: 11 RVA: 0x000021DF File Offset: 0x000003DF
		public override string Description
		{
			get
			{
				return "可以通过回收、记录已经钓到的鱼类（非任务鱼）并获得奖励";
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600000C RID: 12 RVA: 0x000021E6 File Offset: 0x000003E6
		public override string Name
		{
			get
			{
				return "V1.4 Gofishing 1448";
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600000D RID: 13 RVA: 0x000021ED File Offset: 0x000003ED
		public override Version Version
		{
			get
			{
				return new Version(1, 0, 0, 4);
			}
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000021F8 File Offset: 0x000003F8
		public Gofishing(Main game)
			: base(game)
		{
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002204 File Offset: 0x00000404
		public override void Initialize()
		{
			this._数据库 = TShock.DB;
			SqlTable sqlTable = new SqlTable("渔获统计", new SqlColumn[]
			{
				new SqlColumn("用户名", 253, new int?(32))
				{
					Primary = true
				},
				new SqlColumn("鲈鱼", 3)
				{
					DefaultValue = "0"
				},
				new SqlColumn("鳟鱼", 3)
				{
					DefaultValue = "0"
				},
				new SqlColumn("三文鱼", 3)
				{
					DefaultValue = "0"
				},
				new SqlColumn("大西洋鳕鱼", 3)
				{
					DefaultValue = "0"
				},
				new SqlColumn("金枪鱼", 3)
				{
					DefaultValue = "0"
				},
				new SqlColumn("红鲷鱼", 3)
				{
					DefaultValue = "0"
				},
				new SqlColumn("霓虹脂鲤", 3)
				{
					DefaultValue = "0"
				},
				new SqlColumn("装甲洞穴鱼", 3)
				{
					DefaultValue = "0"
				},
				new SqlColumn("雀鲷", 3)
				{
					DefaultValue = "0"
				},
				new SqlColumn("猩红虎鱼", 3)
				{
					DefaultValue = "0"
				},
				new SqlColumn("寒霜鲦鱼", 3)
				{
					DefaultValue = "0"
				},
				new SqlColumn("公主鱼", 3)
				{
					DefaultValue = "0"
				},
				new SqlColumn("金鲤鱼", 3)
				{
					DefaultValue = "0"
				},
				new SqlColumn("镜面鱼", 3)
				{
					DefaultValue = "0"
				},
				new SqlColumn("七彩矿鱼", 3)
				{
					DefaultValue = "0"
				},
				new SqlColumn("斑驳油鱼", 3)
				{
					DefaultValue = "0"
				},
				new SqlColumn("闪鳍锦鲤", 3)
				{
					DefaultValue = "0"
				},
				new SqlColumn("双鳍鳕鱼", 3)
				{
					DefaultValue = "0"
				},
				new SqlColumn("黑曜石鱼", 3)
				{
					DefaultValue = "0"
				},
				new SqlColumn("虾", 3)
				{
					DefaultValue = "0"
				},
				new SqlColumn("混沌鱼", 3)
				{
					DefaultValue = "0"
				},
				new SqlColumn("黑檀锦鲤", 3)
				{
					DefaultValue = "0"
				},
				new SqlColumn("血腥食人鱼", 3)
				{
					DefaultValue = "0"
				},
				new SqlColumn("臭味鱼", 3)
				{
					DefaultValue = "0"
				},
				new SqlColumn("蓝水母", 3)
				{
					DefaultValue = "0"
				},
				new SqlColumn("绿水母", 3)
				{
					DefaultValue = "0"
				},
				new SqlColumn("粉水母", 3)
				{
					DefaultValue = "0"
				},
				new SqlColumn("偏口鱼", 3)
				{
					DefaultValue = "0"
				},
				new SqlColumn("岩龙虾", 3)
				{
					DefaultValue = "0"
				},
				new SqlColumn("总量", 3)
				{
					DefaultValue = "0"
				},
				new SqlColumn("竞赛分", 3)
				{
					DefaultValue = "0"
				},
				new SqlColumn("竞赛排名", 3)
				{
					DefaultValue = "0"
				},
				new SqlColumn("总重量", 3)
				{
					DefaultValue = "0"
				}
			});
			IDbConnection 数据库 = this._数据库;
			IQueryBuilder queryBuilder2;
			if (DbExt.GetSqlType(this._数据库) != 1)
			{
				IQueryBuilder queryBuilder = new MysqlQueryCreator();
				queryBuilder2 = queryBuilder;
			}
			else
			{
				IQueryBuilder queryBuilder = new SqliteQueryCreator();
				queryBuilder2 = queryBuilder;
			}
			SqlTableCreator sqlTableCreator = new SqlTableCreator(数据库, queryBuilder2);
			sqlTableCreator.EnsureTableStructure(sqlTable);
			SqlTable sqlTable2 = new SqlTable("渔获鉴定", new SqlColumn[]
			{
				new SqlColumn("用户名", 752)
				{
					DefaultValue = "0"
				},
				new SqlColumn("鱼ID", 3)
				{
					DefaultValue = "0"
				},
				new SqlColumn("重量", 3)
				{
					DefaultValue = "0"
				},
				new SqlColumn("数量", 3)
				{
					DefaultValue = "0"
				}
			});
			IDbConnection 数据库2 = this._数据库;
			IQueryBuilder queryBuilder3;
			if (DbExt.GetSqlType(this._数据库) != 1)
			{
				IQueryBuilder queryBuilder = new MysqlQueryCreator();
				queryBuilder3 = queryBuilder;
			}
			else
			{
				IQueryBuilder queryBuilder = new SqliteQueryCreator();
				queryBuilder3 = queryBuilder;
			}
			SqlTableCreator sqlTableCreator2 = new SqlTableCreator(数据库2, queryBuilder3);
			sqlTableCreator2.EnsureTableStructure(sqlTable2);
			SqlTable sqlTable3 = new SqlTable("渔获最大", new SqlColumn[]
			{
				new SqlColumn("用户名", 752)
				{
					DefaultValue = "0"
				},
				new SqlColumn("鱼ID", 3)
				{
					DefaultValue = "0"
				},
				new SqlColumn("最大重量", 3)
				{
					DefaultValue = "0"
				}
			});
			IDbConnection 数据库3 = this._数据库;
			IQueryBuilder queryBuilder4;
			if (DbExt.GetSqlType(this._数据库) != 1)
			{
				IQueryBuilder queryBuilder = new MysqlQueryCreator();
				queryBuilder4 = queryBuilder;
			}
			else
			{
				IQueryBuilder queryBuilder = new SqliteQueryCreator();
				queryBuilder4 = queryBuilder;
			}
			SqlTableCreator sqlTableCreator3 = new SqlTableCreator(数据库3, queryBuilder4);
			sqlTableCreator3.EnsureTableStructure(sqlTable3);
			Commands.ChatCommands.Add(new Command(new List<string> { "gofishing.admin" }, new CommandDelegate(this.Fish), new string[] { "渔获<gf>", "渔获", "gf" }));
			ServerApi.Hooks.NetGetData.Register(this, new HookHandler<GetDataEventArgs>(this.GetData));
			Config.GetConfig();
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000027B8 File Offset: 0x000009B8
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000027D8 File Offset: 0x000009D8
		private async void Fish(CommandArgs args)
		{
			bool flag66 = args.Parameters.Count == 0;
			if (flag66)
			{
				args.Player.SendInfoMessage("[i:2487] /渔获 回收 或 /gf recycle    -----回收鱼并获得对应奖励，奖励请腐竹在配置文件自行添加！");
				args.Player.SendInfoMessage("[i:2487] /渔获 查询 或 /gf check    -----查看鱼类稀有度、竞赛积分");
				args.Player.SendInfoMessage("[i:2487] /渔获 领奖 或 /gf win    -----领取竞赛奖励，\r\n    赢得竞赛第一名的玩家可获得奖励，腐竹需要在鱼类配置表添加奖励：第一名");
				args.Player.SendInfoMessage("[i:2487] /渔获 统计 或 /gf list    -----查看回收了多少鱼类");
				args.Player.SendInfoMessage("[i:2487] /渔获 排行 或 /gf rank    -----查看渔获各类指标排行榜！");
				args.Player.SendInfoMessage("[i:2487] /渔获 鉴定回收 或 /gf identify     -----可以鉴定鱼重量并回收，\r\n    可以把鱼放在第一格，然后[c/FFFFFF:左键使用它]来完成回收！");
				args.Player.SendInfoMessage("[i:2487] /渔获 兑换货币 [提交的鱼数量]  或 /gf cash [num]  -----扣除鱼库存，兑换相对应的SE、棱彩和POBC系统货币！\r\n    如货币没有增加，请确认你是否在使用SE、棱彩或POBC系统");
				args.Player.SendInfoMessage("[i:2487] 帮助：1、回收时，请把鱼放在显示栏第一格，并滚轮选中变黄底；\r\n[i:2487]         2、合并分堆，否则可能会重复回收，导致数据不准确！\r\n[i:2487]         3、插件有问题可联系作者：大豆子QQ424259518");
				bool flag67 = args.Player.HasPermission("gofishing.game");
				if (flag67)
				{
					args.Player.SendInfoMessage("[i:2487] /渔获 竞赛 [时长/分钟] 或 /gf game [time/m]   -----开启钓鱼竞赛，规定时间内，竞赛分越多者获胜！");
					args.Player.SendInfoMessage("[i:2487] /渔获 重置竞赛分 或 /gf regame          -----可以重置数据库中的竞赛分和竞赛排名");
				}
			}
			else
			{
				string text47 = args.Parameters[0];
				string text48 = text47;
				string text49 = text48;
				uint num34 = <PrivateImplementationDetails>.ComputeStringHash(text49);
				int num35;
				if (num34 <= 2519019730U)
				{
					if (num34 <= 1112135431U)
					{
						if (num34 <= 200788825U)
						{
							if (num34 != 19860733U)
							{
								if (num34 != 200788825U)
								{
									goto IL_12A2E;
								}
								if (!(text49 == "win"))
								{
									goto IL_12A2E;
								}
							}
							else if (!(text49 == "领奖"))
							{
								goto IL_12A2E;
							}
							using (QueryResult _表 = DbExt.QueryReader(this._数据库, "SELECT * FROM 渔获统计 WHERE 用户名=@0", new object[] { args.Player.Name }))
							{
								bool flag68 = !_表.Read();
								if (flag68)
								{
									args.Player.SendErrorMessage("[i:2487] 对不起！你还没有渔获记录，不能参与领奖！ [i:2487]");
									goto IL_12A2E;
								}
								string _用户名 = _表.Get<string>("用户名");
								int 排名 = _表.Get<int>("竞赛排名");
								int 分 = _表.Get<int>("竞赛分");
								bool flag69 = 排名 == 0;
								if (flag69)
								{
									args.Player.SendErrorMessage("[i:2487] 对不起！钓鱼竞赛还没有开始或者正在进行没出结果，不能领奖！ [i:2487]");
									goto IL_12A2E;
								}
								bool flag70 = 分 == 0;
								if (flag70)
								{
									args.Player.SendErrorMessage("[i:2487] 对不起！你在竞赛期间没有回收任何鱼！无法参与领奖！ [i:2487]");
									goto IL_12A2E;
								}
							}
							QueryResult _表 = null;
							try
							{
								string text = "[i:2487][c/FF8C00:======↓渔获排行↓=========][i:2487]\n";
								int num = 1;
								using (QueryResult queryResult = DbExt.QueryReader(this._数据库, "SELECT * FROM 渔获统计 ORDER BY 竞赛分 ASC", Array.Empty<object>()))
								{
									while (queryResult.Read())
									{
										num35 = num;
										num = num35 + 1;
									}
								}
								QueryResult queryResult = null;
								bool flag = false;
								using (QueryResult queryResult2 = DbExt.QueryReader(this._数据库, "SELECT * FROM 渔获统计 ORDER BY 竞赛分 ASC", Array.Empty<object>()))
								{
									while (queryResult2.Read())
									{
										num35 = num;
										num = num35 - 1;
										flag = true;
										string 用户名 = queryResult2.Get<string>("用户名");
										int 竞赛分 = queryResult2.Get<int>("竞赛分");
										text += string.Format("[c/FFD700:{0}][c/8A2BE2:{1}]→渔获总数：[c/00BFFF:{2}]  尾\n", "【第" + num.ToString() + "名】", 用户名, 竞赛分);
										bool flag71 = "1".Equals(num.ToString());
										if (flag71)
										{
											string 第一名 = 用户名;
											bool flag72 = !(第一名 == args.Player.Name);
											if (flag72)
											{
												args.Player.SendErrorMessage("[i:2487] 对不起！你没有获得钓鱼竞赛第一名，不能领奖！ [i:2487]");
												return;
											}
											第一名 = null;
										}
										bool flag73 = "1".Equals(num.ToString()) && args.Player.Name == 用户名;
										if (flag73)
										{
											foreach (渔获奖励 i in Config.GetConfig().渔获奖励)
											{
												bool flag74 = i.鱼类ID作为奖励组 == "第一名";
												if (flag74)
												{
													foreach (Gofishing.Item1 it in i.包含物品)
													{
														args.Player.GiveItem(it.NetID, it.Stack, it.Perfix);
														args.Player.SendSuccessMessage("[i:2487]恭喜你获得了钓鱼竞赛第一名奖励[i:2487]");
														string 播报 = "/bc [i:2487]恭喜 [c/FFD700:" + 用户名 + "] 获得钓鱼竞赛奖励！钓鱼竞赛活动结束，感谢大家参与！[i:2487]";
														Commands.HandleCommand(TSPlayer.Server, 播报);
														string 播报2 = "/bc [i:2487]由于奖励已发放，已将所有玩家的竞赛分、竞赛排名重置！[i:2487]";
														Commands.HandleCommand(TSPlayer.Server, 播报2);
														播报 = null;
														播报2 = null;
														it = null;
													}
													Gofishing.Item1[] array2 = null;
												}
												i = null;
											}
											渔获奖励[] array = null;
										}
										用户名 = null;
									}
									bool flag75 = flag;
									if (flag75)
									{
										text += "[i:2487][c/FF8C00:======↑渔获排行↑=========][i:2487]";
										args.Player.SendSuccessMessage(text);
										Commands.HandleCommand(TSPlayer.Server, "/渔获 重置竞赛分");
										goto IL_12A2E;
									}
								}
								QueryResult queryResult2 = null;
								text = null;
							}
							catch (Exception ex39)
							{
								Exception ex = ex39;
								TShock.Log.ConsoleError(ex.ToString());
							}
							goto IL_12A2E;
						}
						if (num34 != 217798785U)
						{
							if (num34 != 1112135431U)
							{
								goto IL_12A2E;
							}
							if (!(text49 == "game"))
							{
								goto IL_12A2E;
							}
							goto IL_11F27;
						}
						else
						{
							if (!(text49 == "list"))
							{
								goto IL_12A2E;
							}
							goto IL_C0A0;
						}
					}
					else
					{
						if (num34 <= 1906147643U)
						{
							if (num34 != 1350770625U)
							{
								if (num34 != 1906147643U)
								{
									goto IL_12A2E;
								}
								if (!(text49 == "排行"))
								{
									goto IL_12A2E;
								}
								goto IL_D18A;
							}
							else if (!(text49 == "重置竞赛分"))
							{
								goto IL_12A2E;
							}
						}
						else if (num34 != 2080872981U)
						{
							if (num34 != 2505501938U)
							{
								if (num34 != 2519019730U)
								{
									goto IL_12A2E;
								}
								if (!(text49 == "cash"))
								{
									goto IL_12A2E;
								}
								goto IL_CCD4;
							}
							else if (!(text49 == "regame"))
							{
								goto IL_12A2E;
							}
						}
						else
						{
							if (!(text49 == "回收"))
							{
								goto IL_12A2E;
							}
							goto IL_42E;
						}
						bool flag76 = !args.Player.HasPermission("gofishing.game");
						if (flag76)
						{
							args.Player.SendErrorMessage("[i:2487] 你没有重置竞赛分权限！ [i:2487]");
							return;
						}
						DbExt.Query(TShock.DB, "UPDATE 渔获统计 SET 竞赛分 = @0 ", new object[] { 0 });
						DbExt.Query(TShock.DB, "UPDATE 渔获统计 SET 竞赛排名 = @0 ", new object[] { 0 });
						args.Player.SendSuccessMessage("[i:2487]所有玩家竞赛分已被重置[i:2487]");
						goto IL_12A2E;
					}
				}
				else
				{
					if (num34 <= 3010534471U)
					{
						if (num34 <= 2893713106U)
						{
							if (num34 != 2577259942U)
							{
								if (num34 != 2893713106U)
								{
									goto IL_12A2E;
								}
								if (!(text49 == "兑换货币"))
								{
									goto IL_12A2E;
								}
								goto IL_CCD4;
							}
							else if (!(text49 == "查询"))
							{
								goto IL_12A2E;
							}
						}
						else if (num34 != 2895879719U)
						{
							if (num34 != 3010534471U)
							{
								goto IL_12A2E;
							}
							if (!(text49 == "check"))
							{
								goto IL_12A2E;
							}
						}
						else
						{
							if (!(text49 == "rank"))
							{
								goto IL_12A2E;
							}
							goto IL_D18A;
						}
						args.Player.SendInfoMessage("[c/FF1493:======稀有度===竞赛积分======]");
						args.Player.SendInfoMessage("稀有度：[c/FFFFFF:白色]===[c/FFFFFF:最大66KG]===竞赛积分：1分/尾，如下：");
						args.Player.SendInfoMessage("[i:2299] [i:2290] [i:4401] [i:2302] [i:2301] [i:4402] [i:2316] [i:2298] [i:2297] [i:2300]");
						args.Player.SendInfoMessage("稀有度：[c/1E90FF:蓝色]===[c/1E90FF:最大132KG]===竞赛积分：2分/尾，如下：");
						args.Player.SendInfoMessage("[i:2303] [i:2436] [i:2437] [i:2438] [i:2305] [i:2304] [i:2318] [i:2306] [i:2319] [i:2309] [i:2321] [i:2311] [i:2313]");
						args.Player.SendInfoMessage("稀有度：[c/00FF00:绿色]===[c/00FF00:最大198KG]===竞赛积分：3分/尾，如下：");
						args.Player.SendInfoMessage("[i:2307]   [i:2315]   [i:2312]");
						args.Player.SendInfoMessage("稀有度：[c/FFA500:橙色]===[c/FFA500:最大264KG]===竞赛积分：4分/尾，如下：");
						args.Player.SendInfoMessage("[i:2310]");
						args.Player.SendInfoMessage("稀有度：[c/FF0000:浅红色]===[c/FF0000:最大330KG]===竞赛积分：5分/尾，如下：");
						args.Player.SendInfoMessage("[i:2317]   [i:2308]");
						goto IL_12A2E;
					}
					if (num34 <= 3146463837U)
					{
						if (num34 != 3044070429U)
						{
							if (num34 != 3146463837U)
							{
								goto IL_12A2E;
							}
							if (!(text49 == "统计"))
							{
								goto IL_12A2E;
							}
							goto IL_C0A0;
						}
						else if (!(text49 == "identify"))
						{
							goto IL_12A2E;
						}
					}
					else if (num34 != 3207081579U)
					{
						if (num34 != 3722704622U)
						{
							if (num34 != 3814079790U)
							{
								goto IL_12A2E;
							}
							if (!(text49 == "竞赛"))
							{
								goto IL_12A2E;
							}
							goto IL_11F27;
						}
						else
						{
							if (!(text49 == "recycle"))
							{
								goto IL_12A2E;
							}
							goto IL_42E;
						}
					}
					else if (!(text49 == "鉴定回收"))
					{
						goto IL_12A2E;
					}
					try
					{
						using (QueryResult _表2 = DbExt.QueryReader(this._数据库, "SELECT * FROM 渔获统计 WHERE 用户名=@0", new object[] { args.Player.Name }))
						{
							while (!_表2.Read())
							{
								string _用户名2 = _表2.Get<string>("用户名");
								bool 存在数据库 = args.Player.Name == _用户名2;
								bool flag77 = !存在数据库;
								if (flag77)
								{
									TSPlayer _用户 = args.Player;
									int 鲈鱼 = 0;
									int 鳟鱼 = 0;
									int 三文鱼 = 0;
									int 大西洋鳕鱼 = 0;
									int 金枪鱼 = 0;
									int 红鲷鱼 = 0;
									int 霓虹脂鲤 = 0;
									int 装甲洞穴鱼 = 0;
									int 雀鲷 = 0;
									int 猩红虎鱼 = 0;
									int 寒霜鲦鱼 = 0;
									int 公主鱼 = 0;
									int 金鲤鱼 = 0;
									int 镜面鱼 = 0;
									int 七彩矿鱼 = 0;
									int 斑驳油鱼 = 0;
									int 闪鳍锦鲤 = 0;
									int 双鳍鳕鱼 = 0;
									int 黑曜石鱼 = 0;
									int 虾 = 0;
									int 混沌鱼 = 0;
									int 黑檀锦鲤 = 0;
									int 血腥食人鱼 = 0;
									int 臭味鱼 = 0;
									int 蓝水母 = 0;
									int 绿水母 = 0;
									int 粉水母 = 0;
									int 偏口鱼 = 0;
									int 岩龙虾 = 0;
									int 总量 = 0;
									int 竞赛分2 = 0;
									int 竞赛排名 = 0;
									int 总重量 = 0;
									_用户.SendSuccessMessage(_用户.Name + "这是你第一次使用渔获回收，已自动帮您注册渔获数据。");
									DbExt.Query(this._数据库, "INSERT INTO 渔获统计 (用户名, 鲈鱼, 鳟鱼, 三文鱼, 大西洋鳕鱼, 金枪鱼, 红鲷鱼, 霓虹脂鲤, 装甲洞穴鱼, 雀鲷, 猩红虎鱼, 寒霜鲦鱼, 公主鱼, 金鲤鱼, 镜面鱼, 七彩矿鱼, 斑驳油鱼, 闪鳍锦鲤, 双鳍鳕鱼, 黑曜石鱼, 虾, 混沌鱼, 黑檀锦鲤, 血腥食人鱼, 臭味鱼, 蓝水母, 绿水母, 粉水母, 偏口鱼, 岩龙虾, 总量, 竞赛分, 竞赛排名, 总重量) VALUES (@0, @1, @2, @3, @4, @5, @6, @7, @8, @9, @10, @11, @12, @13, @14, @15, @16, @17, @18, @19, @20, @21, @22, @23, @24, @25, @26, @27, @28, @29, @30, @31, @32, @33);", new object[]
									{
										_用户.Name, 鲈鱼, 鳟鱼, 三文鱼, 大西洋鳕鱼, 金枪鱼, 红鲷鱼, 霓虹脂鲤, 装甲洞穴鱼, 雀鲷,
										猩红虎鱼, 寒霜鲦鱼, 公主鱼, 金鲤鱼, 镜面鱼, 七彩矿鱼, 斑驳油鱼, 闪鳍锦鲤, 双鳍鳕鱼, 黑曜石鱼,
										虾, 混沌鱼, 黑檀锦鲤, 血腥食人鱼, 臭味鱼, 蓝水母, 绿水母, 粉水母, 偏口鱼, 岩龙虾,
										总量, 竞赛分2, 竞赛排名, 总重量
									});
									break;
								}
								_用户名2 = null;
							}
						}
						QueryResult _表2 = null;
					}
					catch (Exception ex39)
					{
						Exception ex2 = ex39;
						TShock.Log.ConsoleError(ex2.ToString());
					}
					int[] item = new int[]
					{
						1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
						1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
						1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
						1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
						1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
						1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
						1, 1, 1, 1, 1, 1, 2, 2, 2, 2,
						2, 2, 2, 2, 2, 2, 2, 2, 2, 2,
						2, 2, 2, 2, 2, 2, 2, 2, 2, 2,
						2, 2, 2, 2, 2, 2, 2, 2, 2, 2,
						2, 2, 2, 2, 2, 2, 2, 2, 2, 2,
						2, 2, 2, 2, 2, 2, 2, 2, 2, 2,
						2, 2, 2, 2, 2, 2, 2, 2, 2, 2,
						2, 3, 3, 3, 3, 3, 3, 3, 3, 3,
						3, 3, 3, 3, 3, 3, 3, 3, 3, 3,
						3, 3, 3, 3, 3, 3, 3, 3, 3, 3,
						3, 3, 3, 3, 3, 3, 3, 3, 3, 3,
						3, 3, 3, 3, 3, 3, 3, 3, 3, 3,
						3, 3, 3, 3, 3, 3, 3, 3, 3, 3,
						3, 3, 3, 3, 3, 4, 4, 4, 4, 4,
						4, 4, 4, 4, 4, 4, 4, 4, 4, 4,
						4, 4, 4, 4, 4, 4, 4, 4, 4, 4,
						4, 4, 4, 4, 4, 4, 4, 4, 4, 4,
						4, 4, 4, 4, 4, 4, 4, 4, 4, 4,
						4, 4, 4, 4, 4, 4, 4, 4, 4, 4,
						4, 4, 4, 4, 4, 4, 4, 4, 5, 5,
						5, 5, 5, 5, 5, 5, 5, 5, 5, 5,
						5, 5, 5, 5, 5, 5, 5, 5, 5, 5,
						5, 5, 5, 5, 5, 5, 5, 5, 5, 5,
						5, 5, 5, 5, 5, 5, 5, 5, 5, 5,
						5, 5, 5, 5, 5, 5, 5, 5, 5, 5,
						5, 5, 5, 5, 5, 5, 5, 5, 5, 5,
						6, 6, 6, 6, 6, 6, 6, 6, 6, 6,
						6, 6, 6, 6, 6, 6, 6, 6, 6, 6,
						6, 6, 6, 6, 6, 6, 6, 6, 6, 6,
						6, 6, 6, 6, 6, 6, 6, 6, 6, 6,
						6, 6, 6, 6, 6, 6, 6, 6, 6, 6,
						6, 6, 6, 6, 6, 6, 6, 6, 6, 6,
						6, 7, 7, 7, 7, 7, 7, 7, 7, 7,
						7, 7, 7, 7, 7, 7, 7, 7, 7, 7,
						7, 7, 7, 7, 7, 7, 7, 7, 7, 7,
						7, 7, 7, 7, 7, 7, 7, 7, 7, 7,
						7, 7, 7, 7, 7, 7, 7, 7, 7, 7,
						7, 7, 7, 7, 7, 7, 7, 7, 7, 7,
						7, 8, 8, 8, 8, 8, 8, 8, 8, 8,
						8, 8, 8, 8, 8, 8, 8, 8, 8, 8,
						8, 8, 8, 8, 8, 8, 8, 8, 8, 8,
						8, 8, 8, 8, 8, 8, 8, 8, 8, 8,
						8, 8, 8, 8, 8, 8, 8, 8, 8, 8,
						8, 8, 8, 8, 8, 8, 8, 8, 8, 8,
						9, 9, 9, 9, 9, 9, 9, 9, 9, 9,
						9, 9, 9, 9, 9, 9, 9, 9, 9, 9,
						9, 9, 9, 9, 9, 9, 9, 9, 9, 9,
						9, 9, 9, 9, 9, 9, 9, 9, 9, 9,
						9, 9, 9, 9, 9, 9, 9, 9, 9, 9,
						9, 9, 9, 9, 9, 9, 9, 9, 10, 10,
						10, 10, 10, 10, 10, 10, 10, 10, 10, 10,
						10, 10, 10, 10, 10, 10, 10, 10, 10, 10,
						10, 10, 10, 10, 10, 10, 10, 10, 10, 10,
						10, 10, 10, 10, 10, 10, 10, 10, 10, 10,
						10, 10, 10, 10, 10, 10, 10, 10, 10, 10,
						10, 10, 10, 10, 10, 11, 11, 11, 11, 11,
						11, 11, 11, 11, 11, 11, 11, 11, 11, 11,
						11, 11, 11, 11, 11, 11, 11, 11, 11, 11,
						11, 11, 11, 11, 11, 11, 11, 11, 11, 11,
						11, 11, 11, 11, 11, 11, 11, 11, 11, 11,
						11, 11, 11, 11, 11, 11, 11, 11, 11, 11,
						11, 12, 12, 12, 12, 12, 12, 12, 12, 12,
						12, 12, 12, 12, 12, 12, 12, 12, 12, 12,
						12, 12, 12, 12, 12, 12, 12, 12, 12, 12,
						12, 12, 12, 12, 12, 12, 12, 12, 12, 12,
						12, 12, 12, 12, 12, 12, 12, 12, 12, 12,
						12, 12, 12, 12, 12, 12, 13, 13, 13, 13,
						13, 13, 13, 13, 13, 13, 13, 13, 13, 13,
						13, 13, 13, 13, 13, 13, 13, 13, 13, 13,
						13, 13, 13, 13, 13, 13, 13, 13, 13, 13,
						13, 13, 13, 13, 13, 13, 13, 13, 13, 13,
						13, 13, 13, 13, 13, 13, 13, 13, 13, 13,
						14, 14, 14, 14, 14, 14, 14, 14, 14, 14,
						14, 14, 14, 14, 14, 14, 14, 14, 14, 14,
						14, 14, 14, 14, 14, 14, 14, 14, 14, 14,
						14, 14, 14, 14, 14, 14, 14, 14, 14, 14,
						14, 14, 14, 14, 14, 14, 14, 14, 14, 14,
						14, 14, 14, 15, 15, 15, 15, 15, 15, 15,
						15, 15, 15, 15, 15, 15, 15, 15, 15, 15,
						15, 15, 15, 15, 15, 15, 15, 15, 15, 15,
						15, 15, 15, 15, 15, 15, 15, 15, 15, 15,
						15, 15, 15, 15, 15, 15, 15, 15, 15, 15,
						15, 15, 15, 15, 15, 16, 16, 16, 16, 16,
						16, 16, 16, 16, 16, 16, 16, 16, 16, 16,
						16, 16, 16, 16, 16, 16, 16, 16, 16, 16,
						16, 16, 16, 16, 16, 16, 16, 16, 16, 16,
						16, 16, 16, 16, 16, 16, 16, 16, 16, 16,
						16, 16, 16, 16, 16, 16, 17, 17, 17, 17,
						17, 17, 17, 17, 17, 17, 17, 17, 17, 17,
						17, 17, 17, 17, 17, 17, 17, 17, 17, 17,
						17, 17, 17, 17, 17, 17, 17, 17, 17, 17,
						17, 17, 17, 17, 17, 17, 17, 17, 17, 17,
						17, 17, 17, 17, 17, 17, 18, 18, 18, 18,
						18, 18, 18, 18, 18, 18, 18, 18, 18, 18,
						18, 18, 18, 18, 18, 18, 18, 18, 18, 18,
						18, 18, 18, 18, 18, 18, 18, 18, 18, 18,
						18, 18, 18, 18, 18, 18, 18, 18, 18, 18,
						18, 18, 18, 18, 18, 19, 19, 19, 19, 19,
						19, 19, 19, 19, 19, 19, 19, 19, 19, 19,
						19, 19, 19, 19, 19, 19, 19, 19, 19, 19,
						19, 19, 19, 19, 19, 19, 19, 19, 19, 19,
						19, 19, 19, 19, 19, 19, 19, 19, 19, 19,
						19, 19, 19, 20, 20, 20, 20, 20, 20, 20,
						20, 20, 20, 20, 20, 20, 20, 20, 20, 20,
						20, 20, 20, 20, 20, 20, 20, 20, 20, 20,
						20, 20, 20, 20, 20, 20, 20, 20, 20, 20,
						20, 20, 20, 20, 20, 20, 20, 20, 20, 20,
						21, 21, 21, 21, 21, 21, 21, 21, 21, 21,
						21, 21, 21, 21, 21, 21, 21, 21, 21, 21,
						21, 21, 21, 21, 21, 21, 21, 21, 21, 21,
						21, 21, 21, 21, 21, 21, 21, 21, 21, 21,
						21, 21, 21, 21, 21, 21, 22, 22, 22, 22,
						22, 22, 22, 22, 22, 22, 22, 22, 22, 22,
						22, 22, 22, 22, 22, 22, 22, 22, 22, 22,
						22, 22, 22, 22, 22, 22, 22, 22, 22, 22,
						22, 22, 22, 22, 22, 22, 22, 22, 22, 22,
						22, 23, 23, 23, 23, 23, 23, 23, 23, 23,
						23, 23, 23, 23, 23, 23, 23, 23, 23, 23,
						23, 23, 23, 23, 23, 23, 23, 23, 23, 23,
						23, 23, 23, 23, 23, 23, 23, 23, 23, 23,
						23, 23, 23, 23, 23, 24, 24, 24, 24, 24,
						24, 24, 24, 24, 24, 24, 24, 24, 24, 24,
						24, 24, 24, 24, 24, 24, 24, 24, 24, 24,
						24, 24, 24, 24, 24, 24, 24, 24, 24, 24,
						24, 24, 24, 24, 24, 24, 24, 24, 25, 25,
						25, 25, 25, 25, 25, 25, 25, 25, 25, 25,
						25, 25, 25, 25, 25, 25, 25, 25, 25, 25,
						25, 25, 25, 25, 25, 25, 25, 25, 25, 25,
						25, 25, 25, 25, 25, 25, 25, 25, 25, 25,
						26, 26, 26, 26, 26, 26, 26, 26, 26, 26,
						26, 26, 26, 26, 26, 26, 26, 26, 26, 26,
						26, 26, 26, 26, 26, 26, 26, 26, 26, 26,
						26, 26, 26, 26, 26, 26, 26, 26, 26, 26,
						26, 27, 27, 27, 27, 27, 27, 27, 27, 27,
						27, 27, 27, 27, 27, 27, 27, 27, 27, 27,
						27, 27, 27, 27, 27, 27, 27, 27, 27, 27,
						27, 27, 27, 27, 27, 27, 27, 27, 27, 27,
						27, 28, 28, 28, 28, 28, 28, 28, 28, 28,
						28, 28, 28, 28, 28, 28, 28, 28, 28, 28,
						28, 28, 28, 28, 28, 28, 28, 28, 28, 28,
						28, 28, 28, 28, 28, 28, 28, 28, 28, 28,
						29, 29, 29, 29, 29, 29, 29, 29, 29, 29,
						29, 29, 29, 29, 29, 29, 29, 29, 29, 29,
						29, 29, 29, 29, 29, 29, 29, 29, 29, 29,
						29, 29, 29, 29, 29, 29, 29, 29, 30, 30,
						30, 30, 30, 30, 30, 30, 30, 30, 30, 30,
						30, 30, 30, 30, 30, 30, 30, 30, 30, 30,
						30, 30, 30, 30, 30, 30, 30, 30, 30, 30,
						30, 30, 30, 30, 30, 31, 31, 31, 31, 31,
						31, 31, 31, 31, 31, 31, 31, 31, 31, 31,
						31, 31, 31, 31, 31, 31, 31, 31, 31, 31,
						31, 31, 31, 31, 31, 31, 31, 31, 31, 31,
						31, 32, 32, 32, 32, 32, 32, 32, 32, 32,
						32, 32, 32, 32, 32, 32, 32, 32, 32, 32,
						32, 32, 32, 32, 32, 32, 32, 32, 32, 32,
						32, 32, 32, 32, 32, 32, 33, 33, 33, 33,
						33, 33, 33, 33, 33, 33, 33, 33, 33, 33,
						33, 33, 33, 33, 33, 33, 33, 33, 33, 33,
						33, 33, 33, 33, 33, 33, 33, 33, 33, 33,
						34, 34, 34, 34, 34, 34, 34, 34, 34, 34,
						34, 34, 34, 34, 34, 34, 34, 34, 34, 34,
						34, 34, 34, 34, 34, 34, 34, 34, 34, 34,
						34, 34, 34, 35, 35, 35, 35, 35, 35, 35,
						35, 35, 35, 35, 35, 35, 35, 35, 35, 35,
						35, 35, 35, 35, 35, 35, 35, 35, 35, 35,
						35, 35, 35, 35, 35, 36, 36, 36, 36, 36,
						36, 36, 36, 36, 36, 36, 36, 36, 36, 36,
						36, 36, 36, 36, 36, 36, 36, 36, 36, 36,
						36, 36, 36, 36, 36, 36, 37, 37, 37, 37,
						37, 37, 37, 37, 37, 37, 37, 37, 37, 37,
						37, 37, 37, 37, 37, 37, 37, 37, 37, 37,
						37, 37, 37, 37, 37, 37, 38, 38, 38, 38,
						38, 38, 38, 38, 38, 38, 38, 38, 38, 38,
						38, 38, 38, 38, 38, 38, 38, 38, 38, 38,
						38, 38, 38, 38, 38, 39, 39, 39, 39, 39,
						39, 39, 39, 39, 39, 39, 39, 39, 39, 39,
						39, 39, 39, 39, 39, 39, 39, 39, 39, 39,
						39, 39, 39, 40, 40, 40, 40, 40, 40, 40,
						40, 40, 40, 40, 40, 40, 40, 40, 40, 40,
						40, 40, 40, 40, 40, 40, 40, 40, 40, 40,
						41, 41, 41, 41, 41, 41, 41, 41, 41, 41,
						41, 41, 41, 41, 41, 41, 41, 41, 41, 41,
						41, 41, 41, 41, 41, 41, 42, 42, 42, 42,
						42, 42, 42, 42, 42, 42, 42, 42, 42, 42,
						42, 42, 42, 42, 42, 42, 42, 42, 42, 42,
						42, 43, 43, 43, 43, 43, 43, 43, 43, 43,
						43, 43, 43, 43, 43, 43, 43, 43, 43, 43,
						43, 43, 43, 43, 43, 44, 44, 44, 44, 44,
						44, 44, 44, 44, 44, 44, 44, 44, 44, 44,
						44, 44, 44, 44, 44, 44, 44, 44, 45, 45,
						45, 45, 45, 45, 45, 45, 45, 45, 45, 45,
						45, 45, 45, 45, 45, 45, 45, 45, 45, 45,
						46, 46, 46, 46, 46, 46, 46, 46, 46, 46,
						46, 46, 46, 46, 46, 46, 46, 46, 46, 46,
						46, 47, 47, 47, 47, 47, 47, 47, 47, 47,
						47, 47, 47, 47, 47, 47, 47, 47, 47, 47,
						47, 48, 48, 48, 48, 48, 48, 48, 48, 48,
						48, 48, 48, 48, 48, 48, 48, 48, 48, 48,
						49, 49, 49, 49, 49, 49, 49, 49, 49, 49,
						49, 49, 49, 49, 49, 49, 49, 49, 50, 50,
						50, 50, 50, 50, 50, 50, 50, 50, 50, 50,
						50, 50, 50, 50, 50, 51, 51, 51, 51, 51,
						51, 51, 51, 51, 51, 51, 51, 51, 51, 51,
						51, 52, 52, 52, 52, 52, 52, 52, 52, 52,
						52, 52, 52, 52, 52, 52, 53, 53, 53, 53,
						53, 53, 53, 53, 53, 53, 53, 53, 53, 53,
						54, 54, 54, 54, 54, 54, 54, 54, 54, 54,
						54, 54, 54, 55, 55, 55, 55, 55, 55, 55,
						55, 55, 55, 55, 55, 56, 56, 56, 56, 56,
						56, 56, 56, 56, 56, 56, 57, 57, 57, 57,
						57, 57, 57, 57, 57, 57, 58, 58, 58, 58,
						58, 58, 58, 58, 58, 59, 59, 59, 59, 59,
						59, 59, 59, 60, 60, 60, 60, 60, 60, 60,
						61, 61, 61, 61, 61, 61, 62, 62, 62, 62,
						62, 63, 63, 63, 63, 64, 64, 64, 65, 65,
						66
					};
					Random ran = new Random();
					int 随机重量 = item[ran.Next(item.Length)];
					TSPlayer tsplayer = args.Player;
					Config config = Config.GetConfig();
					string 用户名2 = args.Player.Name;
					int 手持物品 = args.Player.SelectedItem.netID;
					string 手持物品名字 = args.Player.SelectedItem.Name;
					string 手持物品ID = args.Player.SelectedItem.netID.ToString();
					int 背包第一格数量 = args.Player.SelectedItem.stack;
					int 重量 = 0;
					int 最大重量 = 0;
					using (QueryResult _表3 = DbExt.QueryReader(this._数据库, "SELECT * FROM 渔获鉴定 WHERE (用户名 = @0 AND 鱼ID = @1) AND 重量 = @2", new object[]
					{
						args.Player.Name,
						手持物品,
						随机重量
					}))
					{
						while (_表3.Read())
						{
							重量 = _表3.Get<int>("重量");
						}
					}
					QueryResult _表3 = null;
					using (QueryResult 表 = DbExt.QueryReader(this._数据库, "SELECT * FROM 渔获最大 WHERE 用户名 = @0 AND 鱼ID = @1", new object[]
					{
						args.Player.Name,
						手持物品
					}))
					{
						while (表.Read())
						{
							最大重量 = 表.Get<int>("最大重量");
						}
					}
					QueryResult 表 = null;
					using (QueryResult 表2 = DbExt.QueryReader(this._数据库, "SELECT * FROM 渔获最大 WHERE 用户名 = @0 AND 鱼ID = @1", new object[]
					{
						args.Player.Name,
						手持物品
					}))
					{
						if (!表2.Read())
						{
							DbExt.Query(this._数据库, "INSERT INTO 渔获最大 (用户名, 鱼ID, 最大重量) VALUES (@0, @1, @2);", new object[] { 用户名2, 手持物品, 随机重量 });
							tsplayer.SendSuccessMessage(string.Format("这是你第一次鉴定[I:{0}],已为你建立数据库，请再次鉴定即可生效。", 手持物品));
							return;
						}
					}
					QueryResult 表2 = null;
					bool falg = "2290".Equals(手持物品ID) || "2297".Equals(手持物品ID) || "2298".Equals(手持物品ID) || "2299".Equals(手持物品ID) || "2300".Equals(手持物品ID) || "2301".Equals(手持物品ID) || "2302".Equals(手持物品ID) || "2303".Equals(手持物品ID) || "2304".Equals(手持物品ID) || "2305".Equals(手持物品ID) || "2306".Equals(手持物品ID) || "2307".Equals(手持物品ID) || "2308".Equals(手持物品ID) || "2309".Equals(手持物品ID) || "2310".Equals(手持物品ID) || "2311".Equals(手持物品ID) || "2312".Equals(手持物品ID) || "2313".Equals(手持物品ID) || "2315".Equals(手持物品ID) || "2316".Equals(手持物品ID) || "2317".Equals(手持物品ID) || "2318".Equals(手持物品ID) || "2319".Equals(手持物品ID) || "2321".Equals(手持物品ID) || "2436".Equals(手持物品ID) || "2437".Equals(手持物品ID) || "2438".Equals(手持物品ID) || "4401".Equals(手持物品ID) || "4402".Equals(手持物品ID);
					bool flag78 = !falg;
					if (flag78)
					{
						args.Player.SendErrorMessage("该物品不符合鉴定回收条件（普通鱼）：[i:" + 手持物品ID + "]");
						item = null;
						ran = null;
						tsplayer = null;
						config = null;
						用户名2 = null;
						手持物品ID = null;
						goto IL_12A2E;
					}
					for (int j = 0; j < 1; j = num35 + 1)
					{
						bool flag79 = args.TPlayer.inventory[j].type == 手持物品;
						if (flag79)
						{
							背包第一格数量 = 1;
							int 新数量 = args.TPlayer.inventory[j].stack - 背包第一格数量;
							this.PlayItemSet(args.Player.Index, j, 手持物品, 新数量);
							bool flag80 = "2290".Equals(手持物品ID);
							if (flag80)
							{
								args.Player.SendSuccessMessage(string.Format("已成功回收鱼类：[i:{0}]，回收数量：{1}，鉴定的重量为：{2} KG", 手持物品ID, 背包第一格数量, 随机重量));
								DbExt.Query(TShock.DB, "UPDATE 渔获统计 SET 鲈鱼 = 鲈鱼 + @0 WHERE 用户名 = @1", new object[] { 背包第一格数量, 用户名2 });
								DbExt.Query(TShock.DB, "UPDATE 渔获统计 SET 总量 = 总量 + @0 WHERE 用户名 = @1", new object[] { 背包第一格数量, 用户名2 });
								DbExt.Query(TShock.DB, "UPDATE 渔获统计 SET 竞赛分 = 竞赛分 + @0 WHERE 用户名 = @1", new object[] { 背包第一格数量, 用户名2 });
								DbExt.Query(TShock.DB, "UPDATE 渔获统计 SET 总重量 = 总重量 + @0 WHERE 用户名 = @1", new object[] { 随机重量, 用户名2 });
								bool flag81 = 重量 == 随机重量;
								if (flag81)
								{
									DbExt.Query(TShock.DB, "UPDATE 渔获鉴定 SET 数量 = 数量 + @0 WHERE (用户名 = @1 AND 鱼ID = @2) AND 重量 = @3", new object[] { 1, 用户名2, 手持物品, 随机重量 });
								}
								else
								{
									bool flag82 = 重量 != 随机重量;
									if (flag82)
									{
										DbExt.Query(this._数据库, "INSERT INTO 渔获鉴定 (用户名, 鱼ID, 重量, 数量) VALUES (@0, @1, @2, @3);", new object[] { 用户名2, 手持物品, 随机重量, 背包第一格数量 });
										bool flag83 = 最大重量 < 随机重量;
										if (flag83)
										{
											DbExt.Query(TShock.DB, "UPDATE 渔获最大 SET 最大重量 = @0 WHERE 用户名 = @1 AND 鱼ID = @2", new object[] { 随机重量, 用户名2, 手持物品 });
										}
									}
								}
								foreach (渔获奖励 k in config.渔获奖励)
								{
									bool flag84 = k.鱼类ID作为奖励组 == 手持物品ID;
									if (flag84)
									{
										foreach (Gofishing.Item1 it2 in k.包含物品)
										{
											tsplayer.GiveItem(it2.NetID, it2.Stack * 背包第一格数量, it2.Perfix);
											it2 = null;
										}
										Gofishing.Item1[] array4 = null;
										args.Player.SendSuccessMessage(string.Format("你通过渔获回收，得到了 {1} 个 {0} 奖励", 手持物品ID, 背包第一格数量));
										return;
									}
									k = null;
								}
								渔获奖励[] array3 = null;
								args.Player.SendErrorMessage("腐竹没有设置奖励：" + 手持物品ID + "   如有需要请在配置文件设置!");
								return;
							}
							bool flag85 = "2297".Equals(手持物品ID);
							if (flag85)
							{
								args.Player.SendSuccessMessage(string.Format("已成功回收鱼类：[i:{0}]，回收数量：{1}，鉴定的重量为：{2} KG", 手持物品ID, 背包第一格数量, 随机重量));
								DbExt.Query(TShock.DB, "UPDATE 渔获统计 SET 鳟鱼 = 鳟鱼 + @0 WHERE 用户名 = @1", new object[] { 背包第一格数量, 用户名2 });
								DbExt.Query(TShock.DB, "UPDATE 渔获统计 SET 总量 = 总量 + @0 WHERE 用户名 = @1", new object[] { 背包第一格数量, 用户名2 });
								DbExt.Query(TShock.DB, "UPDATE 渔获统计 SET 竞赛分 = 竞赛分 + @0 WHERE 用户名 = @1", new object[] { 背包第一格数量, 用户名2 });
								DbExt.Query(TShock.DB, "UPDATE 渔获统计 SET 总重量 = 总重量 + @0 WHERE 用户名 = @1", new object[] { 随机重量, 用户名2 });
								bool flag86 = 重量 == 随机重量;
								if (flag86)
								{
									DbExt.Query(TShock.DB, "UPDATE 渔获鉴定 SET 数量 = 数量 + @0 WHERE (用户名 = @1 AND 鱼ID = @2) AND 重量 = @3", new object[] { 1, 用户名2, 手持物品, 随机重量 });
								}
								else
								{
									bool flag87 = 重量 != 随机重量;
									if (flag87)
									{
										DbExt.Query(this._数据库, "INSERT INTO 渔获鉴定 (用户名, 鱼ID, 重量, 数量) VALUES (@0, @1, @2, @3);", new object[] { 用户名2, 手持物品, 随机重量, 背包第一格数量 });
										bool flag88 = 最大重量 < 随机重量;
										if (flag88)
										{
											DbExt.Query(TShock.DB, "UPDATE 渔获最大 SET 最大重量 = @0 WHERE 用户名 = @1 AND 鱼ID = @2", new object[] { 随机重量, 用户名2, 手持物品 });
										}
									}
								}
								foreach (渔获奖励 l in config.渔获奖励)
								{
									bool flag89 = l.鱼类ID作为奖励组 == 手持物品ID;
									if (flag89)
									{
										foreach (Gofishing.Item1 it3 in l.包含物品)
										{
											tsplayer.GiveItem(it3.NetID, it3.Stack * 背包第一格数量, it3.Perfix);
											it3 = null;
										}
										Gofishing.Item1[] array6 = null;
										args.Player.SendSuccessMessage(string.Format("你通过渔获回收，得到了 {1} 个 {0} 奖励", 手持物品ID, 背包第一格数量));
										return;
									}
									l = null;
								}
								渔获奖励[] array5 = null;
								args.Player.SendErrorMessage("腐竹没有设置奖励：" + 手持物品ID + "   如有需要请在配置文件设置!");
								return;
							}
							bool flag90 = "2298".Equals(手持物品ID);
							if (flag90)
							{
								args.Player.SendSuccessMessage(string.Format("已成功回收鱼类：[i:{0}]，回收数量：{1}，鉴定的重量为：{2} KG", 手持物品ID, 背包第一格数量, 随机重量));
								DbExt.Query(TShock.DB, "UPDATE 渔获统计 SET 三文鱼 = 三文鱼 + @0 WHERE 用户名 = @1", new object[] { 背包第一格数量, 用户名2 });
								DbExt.Query(TShock.DB, "UPDATE 渔获统计 SET 总量 = 总量 + @0 WHERE 用户名 = @1", new object[] { 背包第一格数量, 用户名2 });
								DbExt.Query(TShock.DB, "UPDATE 渔获统计 SET 竞赛分 = 竞赛分 + @0 WHERE 用户名 = @1", new object[] { 背包第一格数量, 用户名2 });
								DbExt.Query(TShock.DB, "UPDATE 渔获统计 SET 总重量 = 总重量 + @0 WHERE 用户名 = @1", new object[] { 随机重量, 用户名2 });
								bool flag91 = 重量 == 随机重量;
								if (flag91)
								{
									DbExt.Query(TShock.DB, "UPDATE 渔获鉴定 SET 数量 = 数量 + @0 WHERE (用户名 = @1 AND 鱼ID = @2) AND 重量 = @3", new object[] { 1, 用户名2, 手持物品, 随机重量 });
								}
								else
								{
									bool flag92 = 重量 != 随机重量;
									if (flag92)
									{
										DbExt.Query(this._数据库, "INSERT INTO 渔获鉴定 (用户名, 鱼ID, 重量, 数量) VALUES (@0, @1, @2, @3);", new object[] { 用户名2, 手持物品, 随机重量, 背包第一格数量 });
										bool flag93 = 最大重量 < 随机重量;
										if (flag93)
										{
											DbExt.Query(TShock.DB, "UPDATE 渔获最大 SET 最大重量 = @0 WHERE 用户名 = @1 AND 鱼ID = @2", new object[] { 随机重量, 用户名2, 手持物品 });
										}
									}
								}
								foreach (渔获奖励 m in config.渔获奖励)
								{
									bool flag94 = m.鱼类ID作为奖励组 == 手持物品ID;
									if (flag94)
									{
										foreach (Gofishing.Item1 it4 in m.包含物品)
										{
											tsplayer.GiveItem(it4.NetID, it4.Stack * 背包第一格数量, it4.Perfix);
											it4 = null;
										}
										Gofishing.Item1[] array8 = null;
										args.Player.SendSuccessMessage(string.Format("你通过渔获回收，得到了 {1} 个 {0} 奖励", 手持物品ID, 背包第一格数量));
										return;
									}
									m = null;
								}
								渔获奖励[] array7 = null;
								args.Player.SendErrorMessage("腐竹没有设置奖励：" + 手持物品ID + "   如有需要请在配置文件设置!");
								return;
							}
							bool flag95 = "2299".Equals(手持物品ID);
							if (flag95)
							{
								args.Player.SendSuccessMessage(string.Format("已成功回收鱼类：[i:{0}]，回收数量：{1}，鉴定的重量为：{2} KG", 手持物品ID, 背包第一格数量, 随机重量));
								DbExt.Query(TShock.DB, "UPDATE 渔获统计 SET 大西洋鳕鱼 = 大西洋鳕鱼 + @0 WHERE 用户名 = @1", new object[] { 背包第一格数量, 用户名2 });
								DbExt.Query(TShock.DB, "UPDATE 渔获统计 SET 总量 = 总量 + @0 WHERE 用户名 = @1", new object[] { 背包第一格数量, 用户名2 });
								DbExt.Query(TShock.DB, "UPDATE 渔获统计 SET 竞赛分 = 竞赛分 + @0 WHERE 用户名 = @1", new object[] { 背包第一格数量, 用户名2 });
								DbExt.Query(TShock.DB, "UPDATE 渔获统计 SET 总重量 = 总重量 + @0 WHERE 用户名 = @1", new object[] { 随机重量, 用户名2 });
								bool flag96 = 重量 == 随机重量;
								if (flag96)
								{
									DbExt.Query(TShock.DB, "UPDATE 渔获鉴定 SET 数量 = 数量 + @0 WHERE (用户名 = @1 AND 鱼ID = @2) AND 重量 = @3", new object[] { 1, 用户名2, 手持物品, 随机重量 });
								}
								else
								{
									bool flag97 = 重量 != 随机重量;
									if (flag97)
									{
										DbExt.Query(this._数据库, "INSERT INTO 渔获鉴定 (用户名, 鱼ID, 重量, 数量) VALUES (@0, @1, @2, @3);", new object[] { 用户名2, 手持物品, 随机重量, 背包第一格数量 });
										bool flag98 = 最大重量 < 随机重量;
										if (flag98)
										{
											DbExt.Query(TShock.DB, "UPDATE 渔获最大 SET 最大重量 = @0 WHERE 用户名 = @1 AND 鱼ID = @2", new object[] { 随机重量, 用户名2, 手持物品 });
										}
									}
								}
								foreach (渔获奖励 n in config.渔获奖励)
								{
									bool flag99 = n.鱼类ID作为奖励组 == 手持物品ID;
									if (flag99)
									{
										foreach (Gofishing.Item1 it5 in n.包含物品)
										{
											tsplayer.GiveItem(it5.NetID, it5.Stack * 背包第一格数量, it5.Perfix);
											it5 = null;
										}
										Gofishing.Item1[] array10 = null;
										args.Player.SendSuccessMessage(string.Format("你通过渔获回收，得到了 {1} 个 {0} 奖励", 手持物品ID, 背包第一格数量));
										return;
									}
									n = null;
								}
								渔获奖励[] array9 = null;
								args.Player.SendErrorMessage("腐竹没有设置奖励：" + 手持物品ID + "   如有需要请在配置文件设置!");
								return;
							}
							bool flag100 = "2300".Equals(手持物品ID);
							if (flag100)
							{
								args.Player.SendSuccessMessage(string.Format("已成功回收鱼类：[i:{0}]，回收数量：{1}，鉴定的重量为：{2} KG", 手持物品ID, 背包第一格数量, 随机重量));
								DbExt.Query(TShock.DB, "UPDATE 渔获统计 SET 金枪鱼 = 金枪鱼 + @0 WHERE 用户名 = @1", new object[] { 背包第一格数量, 用户名2 });
								DbExt.Query(TShock.DB, "UPDATE 渔获统计 SET 总量 = 总量 + @0 WHERE 用户名 = @1", new object[] { 背包第一格数量, 用户名2 });
								DbExt.Query(TShock.DB, "UPDATE 渔获统计 SET 竞赛分 = 竞赛分 + @0 WHERE 用户名 = @1", new object[] { 背包第一格数量, 用户名2 });
								DbExt.Query(TShock.DB, "UPDATE 渔获统计 SET 总重量 = 总重量 + @0 WHERE 用户名 = @1", new object[] { 随机重量, 用户名2 });
								bool flag101 = 重量 == 随机重量;
								if (flag101)
								{
									DbExt.Query(TShock.DB, "UPDATE 渔获鉴定 SET 数量 = 数量 + @0 WHERE (用户名 = @1 AND 鱼ID = @2) AND 重量 = @3", new object[] { 1, 用户名2, 手持物品, 随机重量 });
								}
								else
								{
									bool flag102 = 重量 != 随机重量;
									if (flag102)
									{
										DbExt.Query(this._数据库, "INSERT INTO 渔获鉴定 (用户名, 鱼ID, 重量, 数量) VALUES (@0, @1, @2, @3);", new object[] { 用户名2, 手持物品, 随机重量, 背包第一格数量 });
										bool flag103 = 最大重量 < 随机重量;
										if (flag103)
										{
											DbExt.Query(TShock.DB, "UPDATE 渔获最大 SET 最大重量 = @0 WHERE 用户名 = @1 AND 鱼ID = @2", new object[] { 随机重量, 用户名2, 手持物品 });
										}
									}
								}
								foreach (渔获奖励 i2 in config.渔获奖励)
								{
									bool flag104 = i2.鱼类ID作为奖励组 == 手持物品ID;
									if (flag104)
									{
										foreach (Gofishing.Item1 it6 in i2.包含物品)
										{
											tsplayer.GiveItem(it6.NetID, it6.Stack * 背包第一格数量, it6.Perfix);
											it6 = null;
										}
										Gofishing.Item1[] array12 = null;
										args.Player.SendSuccessMessage(string.Format("你通过渔获回收，得到了 {1} 个 {0} 奖励", 手持物品ID, 背包第一格数量));
										return;
									}
									i2 = null;
								}
								渔获奖励[] array11 = null;
								args.Player.SendErrorMessage("腐竹没有设置奖励：" + 手持物品ID + "   如有需要请在配置文件设置!");
								return;
							}
							bool flag105 = "2301".Equals(手持物品ID);
							if (flag105)
							{
								args.Player.SendSuccessMessage(string.Format("已成功回收鱼类：[i:{0}]，回收数量：{1}，鉴定的重量为：{2} KG", 手持物品ID, 背包第一格数量, 随机重量));
								DbExt.Query(TShock.DB, "UPDATE 渔获统计 SET 红鲷鱼= 红鲷鱼 + @0 WHERE 用户名 = @1", new object[] { 背包第一格数量, 用户名2 });
								DbExt.Query(TShock.DB, "UPDATE 渔获统计 SET 总量 = 总量 + @0 WHERE 用户名 = @1", new object[] { 背包第一格数量, 用户名2 });
								DbExt.Query(TShock.DB, "UPDATE 渔获统计 SET 竞赛分 = 竞赛分 + @0 WHERE 用户名 = @1", new object[] { 背包第一格数量, 用户名2 });
								DbExt.Query(TShock.DB, "UPDATE 渔获统计 SET 总重量 = 总重量 + @0 WHERE 用户名 = @1", new object[] { 随机重量, 用户名2 });
								bool flag106 = 重量 == 随机重量;
								if (flag106)
								{
									DbExt.Query(TShock.DB, "UPDATE 渔获鉴定 SET 数量 = 数量 + @0 WHERE (用户名 = @1 AND 鱼ID = @2) AND 重量 = @3", new object[] { 1, 用户名2, 手持物品, 随机重量 });
								}
								else
								{
									bool flag107 = 重量 != 随机重量;
									if (flag107)
									{
										DbExt.Query(this._数据库, "INSERT INTO 渔获鉴定 (用户名, 鱼ID, 重量, 数量) VALUES (@0, @1, @2, @3);", new object[] { 用户名2, 手持物品, 随机重量, 背包第一格数量 });
										bool flag108 = 最大重量 < 随机重量;
										if (flag108)
										{
											DbExt.Query(TShock.DB, "UPDATE 渔获最大 SET 最大重量 = @0 WHERE 用户名 = @1 AND 鱼ID = @2", new object[] { 随机重量, 用户名2, 手持物品 });
										}
									}
								}
								foreach (渔获奖励 i3 in config.渔获奖励)
								{
									bool flag109 = i3.鱼类ID作为奖励组 == 手持物品ID;
									if (flag109)
									{
										foreach (Gofishing.Item1 it7 in i3.包含物品)
										{
											tsplayer.GiveItem(it7.NetID, it7.Stack * 背包第一格数量, it7.Perfix);
											it7 = null;
										}
										Gofishing.Item1[] array14 = null;
										args.Player.SendSuccessMessage(string.Format("你通过渔获回收，得到了 {1} 个 {0} 奖励", 手持物品ID, 背包第一格数量));
										return;
									}
									i3 = null;
								}
								渔获奖励[] array13 = null;
								args.Player.SendErrorMessage("腐竹没有设置奖励：" + 手持物品ID + "   如有需要请在配置文件设置!");
								return;
							}
							bool flag110 = "2302".Equals(手持物品ID);
							if (flag110)
							{
								args.Player.SendSuccessMessage(string.Format("已成功回收鱼类：[i:{0}]，回收数量：{1}，鉴定的重量为：{2} KG", 手持物品ID, 背包第一格数量, 随机重量));
								DbExt.Query(TShock.DB, "UPDATE 渔获统计 SET 霓虹脂鲤 = 霓虹脂鲤 + @0 WHERE 用户名 = @1", new object[] { 背包第一格数量, 用户名2 });
								DbExt.Query(TShock.DB, "UPDATE 渔获统计 SET 总量 = 总量 + @0 WHERE 用户名 = @1", new object[] { 背包第一格数量, 用户名2 });
								DbExt.Query(TShock.DB, "UPDATE 渔获统计 SET 竞赛分 = 竞赛分 + @0 WHERE 用户名 = @1", new object[] { 背包第一格数量, 用户名2 });
								DbExt.Query(TShock.DB, "UPDATE 渔获统计 SET 总重量 = 总重量 + @0 WHERE 用户名 = @1", new object[] { 随机重量, 用户名2 });
								bool flag111 = 重量 == 随机重量;
								if (flag111)
								{
									DbExt.Query(TShock.DB, "UPDATE 渔获鉴定 SET 数量 = 数量 + @0 WHERE (用户名 = @1 AND 鱼ID = @2) AND 重量 = @3", new object[] { 1, 用户名2, 手持物品, 随机重量 });
								}
								else
								{
									bool flag112 = 重量 != 随机重量;
									if (flag112)
									{
										DbExt.Query(this._数据库, "INSERT INTO 渔获鉴定 (用户名, 鱼ID, 重量, 数量) VALUES (@0, @1, @2, @3);", new object[] { 用户名2, 手持物品, 随机重量, 背包第一格数量 });
										bool flag113 = 最大重量 < 随机重量;
										if (flag113)
										{
											DbExt.Query(TShock.DB, "UPDATE 渔获最大 SET 最大重量 = @0 WHERE 用户名 = @1 AND 鱼ID = @2", new object[] { 随机重量, 用户名2, 手持物品 });
										}
									}
								}
								foreach (渔获奖励 i4 in config.渔获奖励)
								{
									bool flag114 = i4.鱼类ID作为奖励组 == 手持物品ID;
									if (flag114)
									{
										foreach (Gofishing.Item1 it8 in i4.包含物品)
										{
											tsplayer.GiveItem(it8.NetID, it8.Stack * 背包第一格数量, it8.Perfix);
											it8 = null;
										}
										Gofishing.Item1[] array16 = null;
										args.Player.SendSuccessMessage(string.Format("你通过渔获回收，得到了 {1} 个 {0} 奖励", 手持物品ID, 背包第一格数量));
										return;
									}
									i4 = null;
								}
								渔获奖励[] array15 = null;
								args.Player.SendErrorMessage("腐竹没有设置奖励：" + 手持物品ID + "   如有需要请在配置文件设置!");
								return;
							}
							bool flag115 = "2303".Equals(手持物品ID);
							if (flag115)
							{
								args.Player.SendSuccessMessage(string.Format("已成功回收鱼类：[i:{0}]，回收数量：{1}，鉴定的重量为：{2} KG", 手持物品ID, 背包第一格数量, 随机重量 * 2));
								DbExt.Query(TShock.DB, "UPDATE 渔获统计 SET 装甲洞穴鱼 = 装甲洞穴鱼 + @0 WHERE 用户名 = @1", new object[] { 背包第一格数量, 用户名2 });
								DbExt.Query(TShock.DB, "UPDATE 渔获统计 SET 总量 = 总量 + @0 WHERE 用户名 = @1", new object[] { 背包第一格数量, 用户名2 });
								DbExt.Query(TShock.DB, "UPDATE 渔获统计 SET 竞赛分 = 竞赛分 + @0 WHERE 用户名 = @1", new object[]
								{
									背包第一格数量 * 2,
									用户名2
								});
								DbExt.Query(TShock.DB, "UPDATE 渔获统计 SET 总重量 = 总重量 + @0 WHERE 用户名 = @1", new object[]
								{
									随机重量 * 2,
									用户名2
								});
								bool flag116 = 重量 == 随机重量 * 2;
								if (flag116)
								{
									DbExt.Query(TShock.DB, "UPDATE 渔获鉴定 SET 数量 = 数量 + @0 WHERE (用户名 = @1 AND 鱼ID = @2) AND 重量 = @3", new object[]
									{
										1,
										用户名2,
										手持物品,
										随机重量 * 2
									});
								}
								else
								{
									bool flag117 = 重量 != 随机重量 * 2;
									if (flag117)
									{
										DbExt.Query(this._数据库, "INSERT INTO 渔获鉴定 (用户名, 鱼ID, 重量, 数量) VALUES (@0, @1, @2, @3);", new object[]
										{
											用户名2,
											手持物品,
											随机重量 * 2,
											背包第一格数量
										});
										bool flag118 = 最大重量 < 随机重量 * 2;
										if (flag118)
										{
											DbExt.Query(TShock.DB, "UPDATE 渔获最大 SET 最大重量 = @0 WHERE 用户名 = @1 AND 鱼ID = @2", new object[]
											{
												随机重量 * 2,
												用户名2,
												手持物品
											});
										}
									}
								}
								foreach (渔获奖励 i5 in config.渔获奖励)
								{
									bool flag119 = i5.鱼类ID作为奖励组 == 手持物品ID;
									if (flag119)
									{
										foreach (Gofishing.Item1 it9 in i5.包含物品)
										{
											tsplayer.GiveItem(it9.NetID, it9.Stack * 背包第一格数量, it9.Perfix);
											it9 = null;
										}
										Gofishing.Item1[] array18 = null;
										args.Player.SendSuccessMessage(string.Format("你通过渔获回收，得到了 {1} 个 {0} 奖励", 手持物品ID, 背包第一格数量));
										return;
									}
									i5 = null;
								}
								渔获奖励[] array17 = null;
								args.Player.SendErrorMessage("腐竹没有设置奖励：" + 手持物品ID + "   如有需要请在配置文件设置!");
								return;
							}
							bool flag120 = "2304".Equals(手持物品ID);
							if (flag120)
							{
								args.Player.SendSuccessMessage(string.Format("已成功回收鱼类：[i:{0}]，回收数量：{1}，鉴定的重量为：{2} KG", 手持物品ID, 背包第一格数量, 随机重量 * 2));
								DbExt.Query(TShock.DB, "UPDATE 渔获统计 SET 雀鲷 = 雀鲷 + @0 WHERE 用户名 = @1", new object[] { 背包第一格数量, 用户名2 });
								DbExt.Query(TShock.DB, "UPDATE 渔获统计 SET 总量 = 总量 + @0 WHERE 用户名 = @1", new object[] { 背包第一格数量, 用户名2 });
								DbExt.Query(TShock.DB, "UPDATE 渔获统计 SET 竞赛分 = 竞赛分 + @0 WHERE 用户名 = @1", new object[]
								{
									背包第一格数量 * 2,
									用户名2
								});
								DbExt.Query(TShock.DB, "UPDATE 渔获统计 SET 总重量 = 总重量 + @0 WHERE 用户名 = @1", new object[]
								{
									随机重量 * 2,
									用户名2
								});
								bool flag121 = 重量 == 随机重量 * 2;
								if (flag121)
								{
									DbExt.Query(TShock.DB, "UPDATE 渔获鉴定 SET 数量 = 数量 + @0 WHERE (用户名 = @1 AND 鱼ID = @2) AND 重量 = @3", new object[]
									{
										1,
										用户名2,
										手持物品,
										随机重量 * 2
									});
								}
								else
								{
									bool flag122 = 重量 != 随机重量 * 2;
									if (flag122)
									{
										DbExt.Query(this._数据库, "INSERT INTO 渔获鉴定 (用户名, 鱼ID, 重量, 数量) VALUES (@0, @1, @2, @3);", new object[]
										{
											用户名2,
											手持物品,
											随机重量 * 2,
											背包第一格数量
										});
										bool flag123 = 最大重量 < 随机重量 * 2;
										if (flag123)
										{
											DbExt.Query(TShock.DB, "UPDATE 渔获最大 SET 最大重量 = @0 WHERE 用户名 = @1 AND 鱼ID = @2", new object[]
											{
												随机重量 * 2,
												用户名2,
												手持物品
											});
										}
									}
								}
								foreach (渔获奖励 i6 in config.渔获奖励)
								{
									bool flag124 = i6.鱼类ID作为奖励组 == 手持物品ID;
									if (flag124)
									{
										foreach (Gofishing.Item1 it10 in i6.包含物品)
										{
											tsplayer.GiveItem(it10.NetID, it10.Stack * 背包第一格数量, it10.Perfix);
											it10 = null;
										}
										Gofishing.Item1[] array20 = null;
										args.Player.SendSuccessMessage(string.Format("你通过渔获回收，得到了 {1} 个 {0} 奖励", 手持物品ID, 背包第一格数量));
										return;
									}
									i6 = null;
								}
								渔获奖励[] array19 = null;
								args.Player.SendErrorMessage("腐竹没有设置奖励：" + 手持物品ID + "   如有需要请在配置文件设置!");
								return;
							}
							bool flag125 = "2305".Equals(手持物品ID);
							if (flag125)
							{
								args.Player.SendSuccessMessage(string.Format("已成功回收鱼类：[i:{0}]，回收数量：{1}，鉴定的重量为：{2} KG", 手持物品ID, 背包第一格数量, 随机重量 * 2));
								DbExt.Query(TShock.DB, "UPDATE 渔获统计 SET 猩红虎鱼 = 猩红虎鱼 + @0 WHERE 用户名 = @1", new object[] { 背包第一格数量, 用户名2 });
								DbExt.Query(TShock.DB, "UPDATE 渔获统计 SET 总量 = 总量 + @0 WHERE 用户名 = @1", new object[] { 背包第一格数量, 用户名2 });
								DbExt.Query(TShock.DB, "UPDATE 渔获统计 SET 竞赛分 = 竞赛分 + @0 WHERE 用户名 = @1", new object[]
								{
									背包第一格数量 * 2,
									用户名2
								});
								DbExt.Query(TShock.DB, "UPDATE 渔获统计 SET 总重量 = 总重量 + @0 WHERE 用户名 = @1", new object[]
								{
									随机重量 * 2,
									用户名2
								});
								bool flag126 = 重量 == 随机重量 * 2;
								if (flag126)
								{
									DbExt.Query(TShock.DB, "UPDATE 渔获鉴定 SET 数量 = 数量 + @0 WHERE (用户名 = @1 AND 鱼ID = @2) AND 重量 = @3", new object[]
									{
										1,
										用户名2,
										手持物品,
										随机重量 * 2
									});
								}
								else
								{
									bool flag127 = 重量 != 随机重量 * 2;
									if (flag127)
									{
										DbExt.Query(this._数据库, "INSERT INTO 渔获鉴定 (用户名, 鱼ID, 重量, 数量) VALUES (@0, @1, @2, @3);", new object[]
										{
											用户名2,
											手持物品,
											随机重量 * 2,
											背包第一格数量
										});
										bool flag128 = 最大重量 < 随机重量 * 2;
										if (flag128)
										{
											DbExt.Query(TShock.DB, "UPDATE 渔获最大 SET 最大重量 = @0 WHERE 用户名 = @1 AND 鱼ID = @2", new object[]
											{
												随机重量 * 2,
												用户名2,
												手持物品
											});
										}
									}
								}
								foreach (渔获奖励 i7 in config.渔获奖励)
								{
									bool flag129 = i7.鱼类ID作为奖励组 == 手持物品ID;
									if (flag129)
									{
										foreach (Gofishing.Item1 it11 in i7.包含物品)
										{
											tsplayer.GiveItem(it11.NetID, it11.Stack * 背包第一格数量, it11.Perfix);
											it11 = null;
										}
										Gofishing.Item1[] array22 = null;
										args.Player.SendSuccessMessage(string.Format("你通过渔获回收，得到了 {1} 个 {0} 奖励", 手持物品ID, 背包第一格数量));
										return;
									}
									i7 = null;
								}
								渔获奖励[] array21 = null;
								args.Player.SendErrorMessage("腐竹没有设置奖励：" + 手持物品ID + "   如有需要请在配置文件设置!");
								return;
							}
							bool flag130 = "2306".Equals(手持物品ID);
							if (flag130)
							{
								args.Player.SendSuccessMessage(string.Format("已成功回收鱼类：[i:{0}]，回收数量：{1}，鉴定的重量为：{2} KG", 手持物品ID, 背包第一格数量, 随机重量 * 2));
								DbExt.Query(TShock.DB, "UPDATE 渔获统计 SET 寒霜鲦鱼 = 寒霜鲦鱼 + @0 WHERE 用户名 = @1", new object[] { 背包第一格数量, 用户名2 });
								DbExt.Query(TShock.DB, "UPDATE 渔获统计 SET 总量 = 总量 + @0 WHERE 用户名 = @1", new object[] { 背包第一格数量, 用户名2 });
								DbExt.Query(TShock.DB, "UPDATE 渔获统计 SET 竞赛分 = 竞赛分 + @0 WHERE 用户名 = @1", new object[]
								{
									背包第一格数量 * 2,
									用户名2
								});
								DbExt.Query(TShock.DB, "UPDATE 渔获统计 SET 总重量 = 总重量 + @0 WHERE 用户名 = @1", new object[]
								{
									随机重量 * 2,
									用户名2
								});
								bool flag131 = 重量 == 随机重量 * 2;
								if (flag131)
								{
									DbExt.Query(TShock.DB, "UPDATE 渔获鉴定 SET 数量 = 数量 + @0 WHERE (用户名 = @1 AND 鱼ID = @2) AND 重量 = @3", new object[]
									{
										1,
										用户名2,
										手持物品,
										随机重量 * 2
									});
								}
								else
								{
									bool flag132 = 重量 != 随机重量 * 2;
									if (flag132)
									{
										DbExt.Query(this._数据库, "INSERT INTO 渔获鉴定 (用户名, 鱼ID, 重量, 数量) VALUES (@0, @1, @2, @3);", new object[]
										{
											用户名2,
											手持物品,
											随机重量 * 2,
											背包第一格数量
										});
										bool flag133 = 最大重量 < 随机重量 * 2;
										if (flag133)
										{
											DbExt.Query(TShock.DB, "UPDATE 渔获最大 SET 最大重量 = @0 WHERE 用户名 = @1 AND 鱼ID = @2", new object[]
											{
												随机重量 * 2,
												用户名2,
												手持物品
											});
										}
									}
								}
								foreach (渔获奖励 i8 in config.渔获奖励)
								{
									bool flag134 = i8.鱼类ID作为奖励组 == 手持物品ID;
									if (flag134)
									{
										foreach (Gofishing.Item1 it12 in i8.包含物品)
										{
											tsplayer.GiveItem(it12.NetID, it12.Stack * 背包第一格数量, it12.Perfix);
											it12 = null;
										}
										Gofishing.Item1[] array24 = null;
										args.Player.SendSuccessMessage(string.Format("你通过渔获回收，得到了 {1} 个 {0} 奖励", 手持物品ID, 背包第一格数量));
										return;
									}
									i8 = null;
								}
								渔获奖励[] array23 = null;
								args.Player.SendErrorMessage("腐竹没有设置奖励：" + 手持物品ID + "   如有需要请在配置文件设置!");
								return;
							}
							bool flag135 = "2307".Equals(手持物品ID);
							if (flag135)
							{
								args.Player.SendSuccessMessage(string.Format("已成功回收鱼类：[i:{0}]，回收数量：{1}，鉴定的重量为：{2} KG", 手持物品ID, 背包第一格数量, 随机重量 * 3));
								DbExt.Query(TShock.DB, "UPDATE 渔获统计 SET 公主鱼 = 公主鱼 + @0 WHERE 用户名 = @1", new object[] { 背包第一格数量, 用户名2 });
								DbExt.Query(TShock.DB, "UPDATE 渔获统计 SET 总量 = 总量 + @0 WHERE 用户名 = @1", new object[] { 背包第一格数量, 用户名2 });
								DbExt.Query(TShock.DB, "UPDATE 渔获统计 SET 竞赛分 = 竞赛分 + @0 WHERE 用户名 = @1", new object[]
								{
									背包第一格数量 * 3,
									用户名2
								});
								DbExt.Query(TShock.DB, "UPDATE 渔获统计 SET 总重量 = 总重量 + @0 WHERE 用户名 = @1", new object[]
								{
									随机重量 * 3,
									用户名2
								});
								bool flag136 = 重量 == 随机重量 * 3;
								if (flag136)
								{
									DbExt.Query(TShock.DB, "UPDATE 渔获鉴定 SET 数量 = 数量 + @0 WHERE (用户名 = @1 AND 鱼ID = @2) AND 重量 = @3", new object[]
									{
										1,
										用户名2,
										手持物品,
										随机重量 * 3
									});
								}
								else
								{
									bool flag137 = 重量 != 随机重量 * 3;
									if (flag137)
									{
										DbExt.Query(this._数据库, "INSERT INTO 渔获鉴定 (用户名, 鱼ID, 重量, 数量) VALUES (@0, @1, @2, @3);", new object[]
										{
											用户名2,
											手持物品,
											随机重量 * 3,
											背包第一格数量
										});
										bool flag138 = 最大重量 < 随机重量 * 3;
										if (flag138)
										{
											DbExt.Query(TShock.DB, "UPDATE 渔获最大 SET 最大重量 = @0 WHERE 用户名 = @1 AND 鱼ID = @2", new object[]
											{
												随机重量 * 3,
												用户名2,
												手持物品
											});
										}
									}
								}
								foreach (渔获奖励 i9 in config.渔获奖励)
								{
									bool flag139 = i9.鱼类ID作为奖励组 == 手持物品ID;
									if (flag139)
									{
										foreach (Gofishing.Item1 it13 in i9.包含物品)
										{
											tsplayer.GiveItem(it13.NetID, it13.Stack * 背包第一格数量, it13.Perfix);
											it13 = null;
										}
										Gofishing.Item1[] array26 = null;
										args.Player.SendSuccessMessage(string.Format("你通过渔获回收，得到了 {1} 个 {0} 奖励", 手持物品ID, 背包第一格数量));
										return;
									}
									i9 = null;
								}
								渔获奖励[] array25 = null;
								args.Player.SendErrorMessage("腐竹没有设置奖励：" + 手持物品ID + "   如有需要请在配置文件设置!");
								return;
							}
							bool flag140 = "2308".Equals(手持物品ID);
							if (flag140)
							{
								args.Player.SendSuccessMessage(string.Format("已成功回收鱼类：[i:{0}]，回收数量：{1}，鉴定的重量为：{2} KG", 手持物品ID, 背包第一格数量, 随机重量 * 5));
								DbExt.Query(TShock.DB, "UPDATE 渔获统计 SET 金鲤鱼 = 金鲤鱼 + @0 WHERE 用户名 = @1", new object[] { 背包第一格数量, 用户名2 });
								DbExt.Query(TShock.DB, "UPDATE 渔获统计 SET 总量 = 总量 + @0 WHERE 用户名 = @1", new object[] { 背包第一格数量, 用户名2 });
								DbExt.Query(TShock.DB, "UPDATE 渔获统计 SET 竞赛分 = 竞赛分 + @0 WHERE 用户名 = @1", new object[]
								{
									背包第一格数量 * 5,
									用户名2
								});
								DbExt.Query(TShock.DB, "UPDATE 渔获统计 SET 总重量 = 总重量 + @0 WHERE 用户名 = @1", new object[]
								{
									随机重量 * 5,
									用户名2
								});
								bool flag141 = 重量 == 随机重量 * 5;
								if (flag141)
								{
									DbExt.Query(TShock.DB, "UPDATE 渔获鉴定 SET 数量 = 数量 + @0 WHERE (用户名 = @1 AND 鱼ID = @2) AND 重量 = @3", new object[]
									{
										1,
										用户名2,
										手持物品,
										随机重量 * 5
									});
								}
								else
								{
									bool flag142 = 重量 != 随机重量 * 5;
									if (flag142)
									{
										DbExt.Query(this._数据库, "INSERT INTO 渔获鉴定 (用户名, 鱼ID, 重量, 数量) VALUES (@0, @1, @2, @3);", new object[]
										{
											用户名2,
											手持物品,
											随机重量 * 5,
											背包第一格数量
										});
										bool flag143 = 最大重量 < 随机重量 * 5;
										if (flag143)
										{
											DbExt.Query(TShock.DB, "UPDATE 渔获最大 SET 最大重量 = @0 WHERE 用户名 = @1 AND 鱼ID = @2", new object[]
											{
												随机重量 * 5,
												用户名2,
												手持物品
											});
										}
									}
								}
								foreach (渔获奖励 i10 in config.渔获奖励)
								{
									bool flag144 = i10.鱼类ID作为奖励组 == 手持物品ID;
									if (flag144)
									{
										foreach (Gofishing.Item1 it14 in i10.包含物品)
										{
											tsplayer.GiveItem(it14.NetID, it14.Stack * 背包第一格数量, it14.Perfix);
											it14 = null;
										}
										Gofishing.Item1[] array28 = null;
										args.Player.SendSuccessMessage(string.Format("你通过渔获回收，得到了 {1} 个 {0} 奖励", 手持物品ID, 背包第一格数量));
										return;
									}
									i10 = null;
								}
								渔获奖励[] array27 = null;
								args.Player.SendErrorMessage("腐竹没有设置奖励：" + 手持物品ID + "   如有需要请在配置文件设置!");
								return;
							}
							bool flag145 = "2309".Equals(手持物品ID);
							if (flag145)
							{
								args.Player.SendSuccessMessage(string.Format("已成功回收鱼类：[i:{0}]，回收数量：{1}，鉴定的重量为：{2} KG", 手持物品ID, 背包第一格数量, 随机重量 * 2));
								DbExt.Query(TShock.DB, "UPDATE 渔获统计 SET 镜面鱼 = 镜面鱼 + @0 WHERE 用户名 = @1", new object[] { 背包第一格数量, 用户名2 });
								DbExt.Query(TShock.DB, "UPDATE 渔获统计 SET 总量 = 总量 + @0 WHERE 用户名 = @1", new object[] { 背包第一格数量, 用户名2 });
								DbExt.Query(TShock.DB, "UPDATE 渔获统计 SET 竞赛分 = 竞赛分 + @0 WHERE 用户名 = @1", new object[]
								{
									背包第一格数量 * 2,
									用户名2
								});
								DbExt.Query(TShock.DB, "UPDATE 渔获统计 SET 总重量 = 总重量 + @0 WHERE 用户名 = @1", new object[]
								{
									随机重量 * 2,
									用户名2
								});
								bool flag146 = 重量 == 随机重量 * 2;
								if (flag146)
								{
									DbExt.Query(TShock.DB, "UPDATE 渔获鉴定 SET 数量 = 数量 + @0 WHERE (用户名 = @1 AND 鱼ID = @2) AND 重量 = @3", new object[]
									{
										1,
										用户名2,
										手持物品,
										随机重量 * 2
									});
								}
								else
								{
									bool flag147 = 重量 != 随机重量 * 2;
									if (flag147)
									{
										DbExt.Query(this._数据库, "INSERT INTO 渔获鉴定 (用户名, 鱼ID, 重量, 数量) VALUES (@0, @1, @2, @3);", new object[]
										{
											用户名2,
											手持物品,
											随机重量 * 2,
											背包第一格数量
										});
										bool flag148 = 最大重量 < 随机重量 * 2;
										if (flag148)
										{
											DbExt.Query(TShock.DB, "UPDATE 渔获最大 SET 最大重量 = @0 WHERE 用户名 = @1 AND 鱼ID = @2", new object[]
											{
												随机重量 * 2,
												用户名2,
												手持物品
											});
										}
									}
								}
								foreach (渔获奖励 i11 in config.渔获奖励)
								{
									bool flag149 = i11.鱼类ID作为奖励组 == 手持物品ID;
									if (flag149)
									{
										foreach (Gofishing.Item1 it15 in i11.包含物品)
										{
											tsplayer.GiveItem(it15.NetID, it15.Stack * 背包第一格数量, it15.Perfix);
											it15 = null;
										}
										Gofishing.Item1[] array30 = null;
										args.Player.SendSuccessMessage(string.Format("你通过渔获回收，得到了 {1} 个 {0} 奖励", 手持物品ID, 背包第一格数量));
										return;
									}
									i11 = null;
								}
								渔获奖励[] array29 = null;
								args.Player.SendErrorMessage("腐竹没有设置奖励：" + 手持物品ID + "   如有需要请在配置文件设置!");
								return;
							}
							bool flag150 = "2310".Equals(手持物品ID);
							if (flag150)
							{
								args.Player.SendSuccessMessage(string.Format("已成功回收鱼类：[i:{0}]，回收数量：{1}，鉴定的重量为：{2} KG", 手持物品ID, 背包第一格数量, 随机重量 * 4));
								DbExt.Query(TShock.DB, "UPDATE 渔获统计 SET 七彩矿鱼 = 七彩矿鱼 + @0 WHERE 用户名 = @1", new object[] { 背包第一格数量, 用户名2 });
								DbExt.Query(TShock.DB, "UPDATE 渔获统计 SET 总量 = 总量 + @0 WHERE 用户名 = @1", new object[] { 背包第一格数量, 用户名2 });
								DbExt.Query(TShock.DB, "UPDATE 渔获统计 SET 竞赛分 = 竞赛分 + @0 WHERE 用户名 = @1", new object[]
								{
									背包第一格数量 * 4,
									用户名2
								});
								DbExt.Query(TShock.DB, "UPDATE 渔获统计 SET 总重量 = 总重量 + @0 WHERE 用户名 = @1", new object[]
								{
									随机重量 * 4,
									用户名2
								});
								bool flag151 = 重量 == 随机重量 * 4;
								if (flag151)
								{
									DbExt.Query(TShock.DB, "UPDATE 渔获鉴定 SET 数量 = 数量 + @0 WHERE (用户名 = @1 AND 鱼ID = @2) AND 重量 = @3", new object[]
									{
										1,
										用户名2,
										手持物品,
										随机重量 * 4
									});
								}
								else
								{
									bool flag152 = 重量 != 随机重量 * 4;
									if (flag152)
									{
										DbExt.Query(this._数据库, "INSERT INTO 渔获鉴定 (用户名, 鱼ID, 重量, 数量) VALUES (@0, @1, @2, @3);", new object[]
										{
											用户名2,
											手持物品,
											随机重量 * 4,
											背包第一格数量
										});
										bool flag153 = 最大重量 < 随机重量 * 4;
										if (flag153)
										{
											DbExt.Query(TShock.DB, "UPDATE 渔获最大 SET 最大重量 = @0 WHERE 用户名 = @1 AND 鱼ID = @2", new object[]
											{
												随机重量 * 4,
												用户名2,
												手持物品
											});
										}
									}
								}
								foreach (渔获奖励 i12 in config.渔获奖励)
								{
									bool flag154 = i12.鱼类ID作为奖励组 == 手持物品ID;
									if (flag154)
									{
										foreach (Gofishing.Item1 it16 in i12.包含物品)
										{
											tsplayer.GiveItem(it16.NetID, it16.Stack * 背包第一格数量, it16.Perfix);
											it16 = null;
										}
										Gofishing.Item1[] array32 = null;
										args.Player.SendSuccessMessage(string.Format("你通过渔获回收，得到了 {1} 个 {0} 奖励", 手持物品ID, 背包第一格数量));
										return;
									}
									i12 = null;
								}
								渔获奖励[] array31 = null;
								args.Player.SendErrorMessage("腐竹没有设置奖励：" + 手持物品ID + "   如有需要请在配置文件设置!");
								return;
							}
							bool flag155 = "2311".Equals(手持物品ID);
							if (flag155)
							{
								args.Player.SendSuccessMessage(string.Format("已成功回收鱼类：[i:{0}]，回收数量：{1}，鉴定的重量为：{2} KG", 手持物品ID, 背包第一格数量, 随机重量 * 2));
								DbExt.Query(TShock.DB, "UPDATE 渔获统计 SET 斑驳油鱼 = 斑驳油鱼 + @0 WHERE 用户名 = @1", new object[] { 背包第一格数量, 用户名2 });
								DbExt.Query(TShock.DB, "UPDATE 渔获统计 SET 总量 = 总量 + @0 WHERE 用户名 = @1", new object[] { 背包第一格数量, 用户名2 });
								DbExt.Query(TShock.DB, "UPDATE 渔获统计 SET 竞赛分 = 竞赛分 + @0 WHERE 用户名 = @1", new object[]
								{
									背包第一格数量 * 2,
									用户名2
								});
								DbExt.Query(TShock.DB, "UPDATE 渔获统计 SET 总重量 = 总重量 + @0 WHERE 用户名 = @1", new object[]
								{
									随机重量 * 2,
									用户名2
								});
								bool flag156 = 重量 == 随机重量 * 2;
								if (flag156)
								{
									DbExt.Query(TShock.DB, "UPDATE 渔获鉴定 SET 数量 = 数量 + @0 WHERE (用户名 = @1 AND 鱼ID = @2) AND 重量 = @3", new object[]
									{
										1,
										用户名2,
										手持物品,
										随机重量 * 2
									});
								}
								else
								{
									bool flag157 = 重量 != 随机重量 * 2;
									if (flag157)
									{
										DbExt.Query(this._数据库, "INSERT INTO 渔获鉴定 (用户名, 鱼ID, 重量, 数量) VALUES (@0, @1, @2, @3);", new object[]
										{
											用户名2,
											手持物品,
											随机重量 * 2,
											背包第一格数量
										});
										bool flag158 = 最大重量 < 随机重量 * 2;
										if (flag158)
										{
											DbExt.Query(TShock.DB, "UPDATE 渔获最大 SET 最大重量 = @0 WHERE 用户名 = @1 AND 鱼ID = @2", new object[]
											{
												随机重量 * 2,
												用户名2,
												手持物品
											});
										}
									}
								}
								foreach (渔获奖励 i13 in config.渔获奖励)
								{
									bool flag159 = i13.鱼类ID作为奖励组 == 手持物品ID;
									if (flag159)
									{
										foreach (Gofishing.Item1 it17 in i13.包含物品)
										{
											tsplayer.GiveItem(it17.NetID, it17.Stack * 背包第一格数量, it17.Perfix);
											it17 = null;
										}
										Gofishing.Item1[] array34 = null;
										args.Player.SendSuccessMessage(string.Format("你通过渔获回收，得到了 {1} 个 {0} 奖励", 手持物品ID, 背包第一格数量));
										return;
									}
									i13 = null;
								}
								渔获奖励[] array33 = null;
								args.Player.SendErrorMessage("腐竹没有设置奖励：" + 手持物品ID + "   如有需要请在配置文件设置!");
								return;
							}
							bool flag160 = "2312".Equals(手持物品ID);
							if (flag160)
							{
								args.Player.SendSuccessMessage(string.Format("已成功回收鱼类：[i:{0}]，回收数量：{1}，鉴定的重量为：{2} KG", 手持物品ID, 背包第一格数量, 随机重量 * 3));
								DbExt.Query(TShock.DB, "UPDATE 渔获统计 SET 闪鳍锦鲤 = 闪鳍锦鲤 + @0 WHERE 用户名 = @1", new object[] { 背包第一格数量, 用户名2 });
								DbExt.Query(TShock.DB, "UPDATE 渔获统计 SET 总量 = 总量 + @0 WHERE 用户名 = @1", new object[] { 背包第一格数量, 用户名2 });
								DbExt.Query(TShock.DB, "UPDATE 渔获统计 SET 竞赛分 = 竞赛分 + @0 WHERE 用户名 = @1", new object[]
								{
									背包第一格数量 * 3,
									用户名2
								});
								DbExt.Query(TShock.DB, "UPDATE 渔获统计 SET 总重量 = 总重量 + @0 WHERE 用户名 = @1", new object[]
								{
									随机重量 * 3,
									用户名2
								});
								bool flag161 = 重量 == 随机重量 * 3;
								if (flag161)
								{
									DbExt.Query(TShock.DB, "UPDATE 渔获鉴定 SET 数量 = 数量 + @0 WHERE (用户名 = @1 AND 鱼ID = @2) AND 重量 = @3", new object[]
									{
										1,
										用户名2,
										手持物品,
										随机重量 * 3
									});
								}
								else
								{
									bool flag162 = 重量 != 随机重量 * 3;
									if (flag162)
									{
										DbExt.Query(this._数据库, "INSERT INTO 渔获鉴定 (用户名, 鱼ID, 重量, 数量) VALUES (@0, @1, @2, @3);", new object[]
										{
											用户名2,
											手持物品,
											随机重量 * 3,
											背包第一格数量
										});
										bool flag163 = 最大重量 < 随机重量 * 3;
										if (flag163)
										{
											DbExt.Query(TShock.DB, "UPDATE 渔获最大 SET 最大重量 = @0 WHERE 用户名 = @1 AND 鱼ID = @2", new object[]
											{
												随机重量 * 3,
												用户名2,
												手持物品
											});
										}
									}
								}
								foreach (渔获奖励 i14 in config.渔获奖励)
								{
									bool flag164 = i14.鱼类ID作为奖励组 == 手持物品ID;
									if (flag164)
									{
										foreach (Gofishing.Item1 it18 in i14.包含物品)
										{
											tsplayer.GiveItem(it18.NetID, it18.Stack * 背包第一格数量, it18.Perfix);
											it18 = null;
										}
										Gofishing.Item1[] array36 = null;
										args.Player.SendSuccessMessage(string.Format("你通过渔获回收，得到了 {1} 个 {0} 奖励", 手持物品ID, 背包第一格数量));
										return;
									}
									i14 = null;
								}
								渔获奖励[] array35 = null;
								args.Player.SendErrorMessage("腐竹没有设置奖励：" + 手持物品ID + "   如有需要请在配置文件设置!");
								return;
							}
							bool flag165 = "2313".Equals(手持物品ID);
							if (flag165)
							{
								args.Player.SendSuccessMessage(string.Format("已成功回收鱼类：[i:{0}]，回收数量：{1}，鉴定的重量为：{2} KG", 手持物品ID, 背包第一格数量, 随机重量 * 2));
								DbExt.Query(TShock.DB, "UPDATE 渔获统计 SET 双鳍鳕鱼 = 双鳍鳕鱼 + @0 WHERE 用户名 = @1", new object[] { 背包第一格数量, 用户名2 });
								DbExt.Query(TShock.DB, "UPDATE 渔获统计 SET 总量 = 总量 + @0 WHERE 用户名 = @1", new object[] { 背包第一格数量, 用户名2 });
								DbExt.Query(TShock.DB, "UPDATE 渔获统计 SET 竞赛分 = 竞赛分 + @0 WHERE 用户名 = @1", new object[]
								{
									背包第一格数量 * 2,
									用户名2
								});
								DbExt.Query(TShock.DB, "UPDATE 渔获统计 SET 总重量 = 总重量 + @0 WHERE 用户名 = @1", new object[]
								{
									随机重量 * 2,
									用户名2
								});
								bool flag166 = 重量 == 随机重量 * 2;
								if (flag166)
								{
									DbExt.Query(TShock.DB, "UPDATE 渔获鉴定 SET 数量 = 数量 + @0 WHERE (用户名 = @1 AND 鱼ID = @2) AND 重量 = @3", new object[]
									{
										1,
										用户名2,
										手持物品,
										随机重量 * 2
									});
								}
								else
								{
									bool flag167 = 重量 != 随机重量 * 2;
									if (flag167)
									{
										DbExt.Query(this._数据库, "INSERT INTO 渔获鉴定 (用户名, 鱼ID, 重量, 数量) VALUES (@0, @1, @2, @3);", new object[]
										{
											用户名2,
											手持物品,
											随机重量 * 2,
											背包第一格数量
										});
										bool flag168 = 最大重量 < 随机重量 * 2;
										if (flag168)
										{
											DbExt.Query(TShock.DB, "UPDATE 渔获最大 SET 最大重量 = @0 WHERE 用户名 = @1 AND 鱼ID = @2", new object[]
											{
												随机重量 * 2,
												用户名2,
												手持物品
											});
										}
									}
								}
								foreach (渔获奖励 i15 in config.渔获奖励)
								{
									bool flag169 = i15.鱼类ID作为奖励组 == 手持物品ID;
									if (flag169)
									{
										foreach (Gofishing.Item1 it19 in i15.包含物品)
										{
											tsplayer.GiveItem(it19.NetID, it19.Stack * 背包第一格数量, it19.Perfix);
											it19 = null;
										}
										Gofishing.Item1[] array38 = null;
										args.Player.SendSuccessMessage(string.Format("你通过渔获回收，得到了 {1} 个 {0} 奖励", 手持物品ID, 背包第一格数量));
										return;
									}
									i15 = null;
								}
								渔获奖励[] array37 = null;
								args.Player.SendErrorMessage("腐竹没有设置奖励：" + 手持物品ID + "   如有需要请在配置文件设置!");
								return;
							}
							bool flag170 = "2315".Equals(手持物品ID);
							if (flag170)
							{
								args.Player.SendSuccessMessage(string.Format("已成功回收鱼类：[i:{0}]，回收数量：{1}，鉴定的重量为：{2} KG", 手持物品ID, 背包第一格数量, 随机重量 * 3));
								DbExt.Query(TShock.DB, "UPDATE 渔获统计 SET 黑曜石鱼 = 黑曜石鱼 + @0 WHERE 用户名 = @1", new object[] { 背包第一格数量, 用户名2 });
								DbExt.Query(TShock.DB, "UPDATE 渔获统计 SET 总量 = 总量 + @0 WHERE 用户名 = @1", new object[] { 背包第一格数量, 用户名2 });
								DbExt.Query(TShock.DB, "UPDATE 渔获统计 SET 竞赛分 = 竞赛分 + @0 WHERE 用户名 = @1", new object[]
								{
									背包第一格数量 * 3,
									用户名2
								});
								DbExt.Query(TShock.DB, "UPDATE 渔获统计 SET 总重量 = 总重量 + @0 WHERE 用户名 = @1", new object[]
								{
									随机重量 * 3,
									用户名2
								});
								bool flag171 = 重量 == 随机重量 * 3;
								if (flag171)
								{
									DbExt.Query(TShock.DB, "UPDATE 渔获鉴定 SET 数量 = 数量 + @0 WHERE (用户名 = @1 AND 鱼ID = @2) AND 重量 = @3", new object[]
									{
										1,
										用户名2,
										手持物品,
										随机重量 * 3
									});
								}
								else
								{
									bool flag172 = 重量 != 随机重量 * 3;
									if (flag172)
									{
										DbExt.Query(this._数据库, "INSERT INTO 渔获鉴定 (用户名, 鱼ID, 重量, 数量) VALUES (@0, @1, @2, @3);", new object[]
										{
											用户名2,
											手持物品,
											随机重量 * 3,
											背包第一格数量
										});
										bool flag173 = 最大重量 < 随机重量 * 3;
										if (flag173)
										{
											DbExt.Query(TShock.DB, "UPDATE 渔获最大 SET 最大重量 = @0 WHERE 用户名 = @1 AND 鱼ID = @2", new object[]
											{
												随机重量 * 3,
												用户名2,
												手持物品
											});
										}
									}
								}
								foreach (渔获奖励 i16 in config.渔获奖励)
								{
									bool flag174 = i16.鱼类ID作为奖励组 == 手持物品ID;
									if (flag174)
									{
										foreach (Gofishing.Item1 it20 in i16.包含物品)
										{
											tsplayer.GiveItem(it20.NetID, it20.Stack * 背包第一格数量, it20.Perfix);
											it20 = null;
										}
										Gofishing.Item1[] array40 = null;
										args.Player.SendSuccessMessage(string.Format("你通过渔获回收，得到了 {1} 个 {0} 奖励", 手持物品ID, 背包第一格数量));
										return;
									}
									i16 = null;
								}
								渔获奖励[] array39 = null;
								args.Player.SendErrorMessage("腐竹没有设置奖励：" + 手持物品ID + "   如有需要请在配置文件设置!");
								return;
							}
							bool flag175 = "2316".Equals(手持物品ID);
							if (flag175)
							{
								args.Player.SendSuccessMessage(string.Format("已成功回收鱼类：[i:{0}]，回收数量：{1}，鉴定的重量为：{2} KG", 手持物品ID, 背包第一格数量, 随机重量));
								DbExt.Query(TShock.DB, "UPDATE 渔获统计 SET 虾 = 虾 + @0 WHERE 用户名 = @1", new object[] { 背包第一格数量, 用户名2 });
								DbExt.Query(TShock.DB, "UPDATE 渔获统计 SET 总量 = 总量 + @0 WHERE 用户名 = @1", new object[] { 背包第一格数量, 用户名2 });
								DbExt.Query(TShock.DB, "UPDATE 渔获统计 SET 竞赛分 = 竞赛分 + @0 WHERE 用户名 = @1", new object[] { 背包第一格数量, 用户名2 });
								DbExt.Query(TShock.DB, "UPDATE 渔获统计 SET 总重量 = 总重量 + @0 WHERE 用户名 = @1", new object[] { 随机重量, 用户名2 });
								bool flag176 = 重量 == 随机重量;
								if (flag176)
								{
									DbExt.Query(TShock.DB, "UPDATE 渔获鉴定 SET 数量 = 数量 + @0 WHERE (用户名 = @1 AND 鱼ID = @2) AND 重量 = @3", new object[] { 1, 用户名2, 手持物品, 随机重量 });
								}
								else
								{
									bool flag177 = 重量 != 随机重量;
									if (flag177)
									{
										DbExt.Query(this._数据库, "INSERT INTO 渔获鉴定 (用户名, 鱼ID, 重量, 数量) VALUES (@0, @1, @2, @3);", new object[] { 用户名2, 手持物品, 随机重量, 背包第一格数量 });
										bool flag178 = 最大重量 < 随机重量;
										if (flag178)
										{
											DbExt.Query(TShock.DB, "UPDATE 渔获最大 SET 最大重量 = @0 WHERE 用户名 = @1 AND 鱼ID = @2", new object[] { 随机重量, 用户名2, 手持物品 });
										}
									}
								}
								foreach (渔获奖励 i17 in config.渔获奖励)
								{
									bool flag179 = i17.鱼类ID作为奖励组 == 手持物品ID;
									if (flag179)
									{
										foreach (Gofishing.Item1 it21 in i17.包含物品)
										{
											tsplayer.GiveItem(it21.NetID, it21.Stack * 背包第一格数量, it21.Perfix);
											it21 = null;
										}
										Gofishing.Item1[] array42 = null;
										args.Player.SendSuccessMessage(string.Format("你通过渔获回收，得到了 {1} 个 {0} 奖励", 手持物品ID, 背包第一格数量));
										return;
									}
									i17 = null;
								}
								渔获奖励[] array41 = null;
								args.Player.SendErrorMessage("腐竹没有设置奖励：" + 手持物品ID + "   如有需要请在配置文件设置!");
								return;
							}
							bool flag180 = "2317".Equals(手持物品ID);
							if (flag180)
							{
								args.Player.SendSuccessMessage(string.Format("已成功回收鱼类：[i:{0}]，回收数量：{1}，鉴定的重量为：{2} KG", 手持物品ID, 背包第一格数量, 随机重量 * 5));
								DbExt.Query(TShock.DB, "UPDATE 渔获统计 SET 混沌鱼 = 混沌鱼 + @0 WHERE 用户名 = @1", new object[] { 背包第一格数量, 用户名2 });
								DbExt.Query(TShock.DB, "UPDATE 渔获统计 SET 总量 = 总量 + @0 WHERE 用户名 = @1", new object[] { 背包第一格数量, 用户名2 });
								DbExt.Query(TShock.DB, "UPDATE 渔获统计 SET 竞赛分 = 竞赛分 + @0 WHERE 用户名 = @1", new object[]
								{
									背包第一格数量 * 5,
									用户名2
								});
								DbExt.Query(TShock.DB, "UPDATE 渔获统计 SET 总重量 = 总重量 + @0 WHERE 用户名 = @1", new object[]
								{
									随机重量 * 5,
									用户名2
								});
								bool flag181 = 重量 == 随机重量 * 5;
								if (flag181)
								{
									DbExt.Query(TShock.DB, "UPDATE 渔获鉴定 SET 数量 = 数量 + @0 WHERE (用户名 = @1 AND 鱼ID = @2) AND 重量 = @3", new object[]
									{
										1,
										用户名2,
										手持物品,
										随机重量 * 5
									});
								}
								else
								{
									bool flag182 = 重量 != 随机重量 * 5;
									if (flag182)
									{
										DbExt.Query(this._数据库, "INSERT INTO 渔获鉴定 (用户名, 鱼ID, 重量, 数量) VALUES (@0, @1, @2, @3);", new object[]
										{
											用户名2,
											手持物品,
											随机重量 * 5,
											背包第一格数量
										});
										bool flag183 = 最大重量 < 随机重量 * 5;
										if (flag183)
										{
											DbExt.Query(TShock.DB, "UPDATE 渔获最大 SET 最大重量 = @0 WHERE 用户名 = @1 AND 鱼ID = @2", new object[]
											{
												随机重量 * 5,
												用户名2,
												手持物品
											});
										}
									}
								}
								foreach (渔获奖励 i18 in config.渔获奖励)
								{
									bool flag184 = i18.鱼类ID作为奖励组 == 手持物品ID;
									if (flag184)
									{
										foreach (Gofishing.Item1 it22 in i18.包含物品)
										{
											tsplayer.GiveItem(it22.NetID, it22.Stack * 背包第一格数量, it22.Perfix);
											it22 = null;
										}
										Gofishing.Item1[] array44 = null;
										args.Player.SendSuccessMessage(string.Format("你通过渔获回收，得到了 {1} 个 {0} 奖励", 手持物品ID, 背包第一格数量));
										return;
									}
									i18 = null;
								}
								渔获奖励[] array43 = null;
								args.Player.SendErrorMessage("腐竹没有设置奖励：" + 手持物品ID + "   如有需要请在配置文件设置!");
								return;
							}
							bool flag185 = "2318".Equals(手持物品ID);
							if (flag185)
							{
								args.Player.SendSuccessMessage(string.Format("已成功回收鱼类：[i:{0}]，回收数量：{1}，鉴定的重量为：{2} KG", 手持物品ID, 背包第一格数量, 随机重量 * 2));
								DbExt.Query(TShock.DB, "UPDATE 渔获统计 SET 黑檀锦鲤 = 黑檀锦鲤 + @0 WHERE 用户名 = @1", new object[] { 背包第一格数量, 用户名2 });
								DbExt.Query(TShock.DB, "UPDATE 渔获统计 SET 总量 = 总量 + @0 WHERE 用户名 = @1", new object[] { 背包第一格数量, 用户名2 });
								DbExt.Query(TShock.DB, "UPDATE 渔获统计 SET 竞赛分 = 竞赛分 + @0 WHERE 用户名 = @1", new object[]
								{
									背包第一格数量 * 2,
									用户名2
								});
								DbExt.Query(TShock.DB, "UPDATE 渔获统计 SET 总重量 = 总重量 + @0 WHERE 用户名 = @1", new object[]
								{
									随机重量 * 2,
									用户名2
								});
								bool flag186 = 重量 == 随机重量 * 2;
								if (flag186)
								{
									DbExt.Query(TShock.DB, "UPDATE 渔获鉴定 SET 数量 = 数量 + @0 WHERE (用户名 = @1 AND 鱼ID = @2) AND 重量 = @3", new object[]
									{
										1,
										用户名2,
										手持物品,
										随机重量 * 2
									});
								}
								else
								{
									bool flag187 = 重量 != 随机重量 * 2;
									if (flag187)
									{
										DbExt.Query(this._数据库, "INSERT INTO 渔获鉴定 (用户名, 鱼ID, 重量, 数量) VALUES (@0, @1, @2, @3);", new object[]
										{
											用户名2,
											手持物品,
											随机重量 * 2,
											背包第一格数量
										});
										bool flag188 = 最大重量 < 随机重量 * 2;
										if (flag188)
										{
											DbExt.Query(TShock.DB, "UPDATE 渔获最大 SET 最大重量 = @0 WHERE 用户名 = @1 AND 鱼ID = @2", new object[]
											{
												随机重量 * 2,
												用户名2,
												手持物品
											});
										}
									}
								}
								foreach (渔获奖励 i19 in config.渔获奖励)
								{
									bool flag189 = i19.鱼类ID作为奖励组 == 手持物品ID;
									if (flag189)
									{
										foreach (Gofishing.Item1 it23 in i19.包含物品)
										{
											tsplayer.GiveItem(it23.NetID, it23.Stack * 背包第一格数量, it23.Perfix);
											it23 = null;
										}
										Gofishing.Item1[] array46 = null;
										args.Player.SendSuccessMessage(string.Format("你通过渔获回收，得到了 {1} 个 {0} 奖励", 手持物品ID, 背包第一格数量));
										return;
									}
									i19 = null;
								}
								渔获奖励[] array45 = null;
								args.Player.SendErrorMessage("腐竹没有设置奖励：" + 手持物品ID + "   如有需要请在配置文件设置!");
								return;
							}
							bool flag190 = "2319".Equals(手持物品ID);
							if (flag190)
							{
								args.Player.SendSuccessMessage(string.Format("已成功回收鱼类：[i:{0}]，回收数量：{1}，鉴定的重量为：{2} KG", 手持物品ID, 背包第一格数量, 随机重量 * 2));
								DbExt.Query(TShock.DB, "UPDATE 渔获统计 SET 血腥食人鱼 = 血腥食人鱼 + @0 WHERE 用户名 = @1", new object[] { 背包第一格数量, 用户名2 });
								DbExt.Query(TShock.DB, "UPDATE 渔获统计 SET 总量 = 总量 + @0 WHERE 用户名 = @1", new object[] { 背包第一格数量, 用户名2 });
								DbExt.Query(TShock.DB, "UPDATE 渔获统计 SET 竞赛分 = 竞赛分 + @0 WHERE 用户名 = @1", new object[]
								{
									背包第一格数量 * 2,
									用户名2
								});
								DbExt.Query(TShock.DB, "UPDATE 渔获统计 SET 总重量 = 总重量 + @0 WHERE 用户名 = @1", new object[]
								{
									随机重量 * 2,
									用户名2
								});
								bool flag191 = 重量 == 随机重量 * 2;
								if (flag191)
								{
									DbExt.Query(TShock.DB, "UPDATE 渔获鉴定 SET 数量 = 数量 + @0 WHERE (用户名 = @1 AND 鱼ID = @2) AND 重量 = @3", new object[]
									{
										1,
										用户名2,
										手持物品,
										随机重量 * 2
									});
								}
								else
								{
									bool flag192 = 重量 != 随机重量 * 2;
									if (flag192)
									{
										DbExt.Query(this._数据库, "INSERT INTO 渔获鉴定 (用户名, 鱼ID, 重量, 数量) VALUES (@0, @1, @2, @3);", new object[]
										{
											用户名2,
											手持物品,
											随机重量 * 2,
											背包第一格数量
										});
										bool flag193 = 最大重量 < 随机重量 * 2;
										if (flag193)
										{
											DbExt.Query(TShock.DB, "UPDATE 渔获最大 SET 最大重量 = @0 WHERE 用户名 = @1 AND 鱼ID = @2", new object[]
											{
												随机重量 * 2,
												用户名2,
												手持物品
											});
										}
									}
								}
								foreach (渔获奖励 i20 in config.渔获奖励)
								{
									bool flag194 = i20.鱼类ID作为奖励组 == 手持物品ID;
									if (flag194)
									{
										foreach (Gofishing.Item1 it24 in i20.包含物品)
										{
											tsplayer.GiveItem(it24.NetID, it24.Stack * 背包第一格数量, it24.Perfix);
											it24 = null;
										}
										Gofishing.Item1[] array48 = null;
										args.Player.SendSuccessMessage(string.Format("你通过渔获回收，得到了 {1} 个 {0} 奖励", 手持物品ID, 背包第一格数量));
										return;
									}
									i20 = null;
								}
								渔获奖励[] array47 = null;
								args.Player.SendErrorMessage("腐竹没有设置奖励：" + 手持物品ID + "   如有需要请在配置文件设置!");
								return;
							}
							bool flag195 = "2321".Equals(手持物品ID);
							if (flag195)
							{
								args.Player.SendSuccessMessage(string.Format("已成功回收鱼类：[i:{0}]，回收数量：{1}，鉴定的重量为：{2} KG", 手持物品ID, 背包第一格数量, 随机重量 * 2));
								DbExt.Query(TShock.DB, "UPDATE 渔获统计 SET 臭味鱼 = 臭味鱼 + @0 WHERE 用户名 = @1", new object[] { 背包第一格数量, 用户名2 });
								DbExt.Query(TShock.DB, "UPDATE 渔获统计 SET 总量 = 总量 + @0 WHERE 用户名 = @1", new object[] { 背包第一格数量, 用户名2 });
								DbExt.Query(TShock.DB, "UPDATE 渔获统计 SET 竞赛分 = 竞赛分 + @0 WHERE 用户名 = @1", new object[]
								{
									背包第一格数量 * 2,
									用户名2
								});
								DbExt.Query(TShock.DB, "UPDATE 渔获统计 SET 总重量 = 总重量 + @0 WHERE 用户名 = @1", new object[]
								{
									随机重量 * 2,
									用户名2
								});
								bool flag196 = 重量 == 随机重量 * 2;
								if (flag196)
								{
									DbExt.Query(TShock.DB, "UPDATE 渔获鉴定 SET 数量 = 数量 + @0 WHERE (用户名 = @1 AND 鱼ID = @2) AND 重量 = @3", new object[]
									{
										1,
										用户名2,
										手持物品,
										随机重量 * 2
									});
								}
								else
								{
									bool flag197 = 重量 != 随机重量 * 2;
									if (flag197)
									{
										DbExt.Query(this._数据库, "INSERT INTO 渔获鉴定 (用户名, 鱼ID, 重量, 数量) VALUES (@0, @1, @2, @3);", new object[]
										{
											用户名2,
											手持物品,
											随机重量 * 2,
											背包第一格数量
										});
										bool flag198 = 最大重量 < 随机重量 * 2;
										if (flag198)
										{
											DbExt.Query(TShock.DB, "UPDATE 渔获最大 SET 最大重量 = @0 WHERE 用户名 = @1 AND 鱼ID = @2", new object[]
											{
												随机重量 * 2,
												用户名2,
												手持物品
											});
										}
									}
								}
								foreach (渔获奖励 i21 in config.渔获奖励)
								{
									bool flag199 = i21.鱼类ID作为奖励组 == 手持物品ID;
									if (flag199)
									{
										foreach (Gofishing.Item1 it25 in i21.包含物品)
										{
											tsplayer.GiveItem(it25.NetID, it25.Stack * 背包第一格数量, it25.Perfix);
											it25 = null;
										}
										Gofishing.Item1[] array50 = null;
										args.Player.SendSuccessMessage(string.Format("你通过渔获回收，得到了 {1} 个 {0} 奖励", 手持物品ID, 背包第一格数量));
										return;
									}
									i21 = null;
								}
								渔获奖励[] array49 = null;
								args.Player.SendErrorMessage("腐竹没有设置奖励：" + 手持物品ID + "   如有需要请在配置文件设置!");
								return;
							}
							bool flag200 = "2436".Equals(手持物品ID);
							if (flag200)
							{
								args.Player.SendSuccessMessage(string.Format("已成功回收鱼类：[i:{0}]，回收数量：{1}，鉴定的重量为：{2} KG", 手持物品ID, 背包第一格数量, 随机重量 * 2));
								DbExt.Query(TShock.DB, "UPDATE 渔获统计 SET 蓝水母 = 蓝水母 + @0 WHERE 用户名 = @1", new object[] { 背包第一格数量, 用户名2 });
								DbExt.Query(TShock.DB, "UPDATE 渔获统计 SET 总量 = 总量 + @0 WHERE 用户名 = @1", new object[] { 背包第一格数量, 用户名2 });
								DbExt.Query(TShock.DB, "UPDATE 渔获统计 SET 竞赛分 = 竞赛分 + @0 WHERE 用户名 = @1", new object[]
								{
									背包第一格数量 * 2,
									用户名2
								});
								DbExt.Query(TShock.DB, "UPDATE 渔获统计 SET 总重量 = 总重量 + @0 WHERE 用户名 = @1", new object[]
								{
									随机重量 * 2,
									用户名2
								});
								bool flag201 = 重量 == 随机重量 * 2;
								if (flag201)
								{
									DbExt.Query(TShock.DB, "UPDATE 渔获鉴定 SET 数量 = 数量 + @0 WHERE (用户名 = @1 AND 鱼ID = @2) AND 重量 = @3", new object[]
									{
										1,
										用户名2,
										手持物品,
										随机重量 * 2
									});
								}
								else
								{
									bool flag202 = 重量 != 随机重量 * 2;
									if (flag202)
									{
										DbExt.Query(this._数据库, "INSERT INTO 渔获鉴定 (用户名, 鱼ID, 重量, 数量) VALUES (@0, @1, @2, @3);", new object[]
										{
											用户名2,
											手持物品,
											随机重量 * 2,
											背包第一格数量
										});
										bool flag203 = 最大重量 < 随机重量 * 2;
										if (flag203)
										{
											DbExt.Query(TShock.DB, "UPDATE 渔获最大 SET 最大重量 = @0 WHERE 用户名 = @1 AND 鱼ID = @2", new object[]
											{
												随机重量 * 2,
												用户名2,
												手持物品
											});
										}
									}
								}
								foreach (渔获奖励 i22 in config.渔获奖励)
								{
									bool flag204 = i22.鱼类ID作为奖励组 == 手持物品ID;
									if (flag204)
									{
										foreach (Gofishing.Item1 it26 in i22.包含物品)
										{
											tsplayer.GiveItem(it26.NetID, it26.Stack * 背包第一格数量, it26.Perfix);
											it26 = null;
										}
										Gofishing.Item1[] array52 = null;
										args.Player.SendSuccessMessage(string.Format("你通过渔获回收，得到了 {1} 个 {0} 奖励", 手持物品ID, 背包第一格数量));
										return;
									}
									i22 = null;
								}
								渔获奖励[] array51 = null;
								args.Player.SendErrorMessage("腐竹没有设置奖励：" + 手持物品ID + "   如有需要请在配置文件设置!");
								return;
							}
							bool flag205 = "2437".Equals(手持物品ID);
							if (flag205)
							{
								args.Player.SendSuccessMessage(string.Format("已成功回收鱼类：[i:{0}]，回收数量：{1}，鉴定的重量为：{2} KG", 手持物品ID, 背包第一格数量, 随机重量 * 2));
								DbExt.Query(TShock.DB, "UPDATE 渔获统计 SET 绿水母 = 绿水母 + @0 WHERE 用户名 = @1", new object[] { 背包第一格数量, 用户名2 });
								DbExt.Query(TShock.DB, "UPDATE 渔获统计 SET 总量 = 总量 + @0 WHERE 用户名 = @1", new object[] { 背包第一格数量, 用户名2 });
								DbExt.Query(TShock.DB, "UPDATE 渔获统计 SET 竞赛分 = 竞赛分 + @0 WHERE 用户名 = @1", new object[]
								{
									背包第一格数量 * 2,
									用户名2
								});
								DbExt.Query(TShock.DB, "UPDATE 渔获统计 SET 总重量 = 总重量 + @0 WHERE 用户名 = @1", new object[]
								{
									随机重量 * 2,
									用户名2
								});
								bool flag206 = 重量 == 随机重量 * 2;
								if (flag206)
								{
									DbExt.Query(TShock.DB, "UPDATE 渔获鉴定 SET 数量 = 数量 + @0 WHERE (用户名 = @1 AND 鱼ID = @2) AND 重量 = @3", new object[]
									{
										1,
										用户名2,
										手持物品,
										随机重量 * 2
									});
								}
								else
								{
									bool flag207 = 重量 != 随机重量 * 2;
									if (flag207)
									{
										DbExt.Query(this._数据库, "INSERT INTO 渔获鉴定 (用户名, 鱼ID, 重量, 数量) VALUES (@0, @1, @2, @3);", new object[]
										{
											用户名2,
											手持物品,
											随机重量 * 2,
											背包第一格数量
										});
										bool flag208 = 最大重量 < 随机重量 * 2;
										if (flag208)
										{
											DbExt.Query(TShock.DB, "UPDATE 渔获最大 SET 最大重量 = @0 WHERE 用户名 = @1 AND 鱼ID = @2", new object[]
											{
												随机重量 * 2,
												用户名2,
												手持物品
											});
										}
									}
								}
								foreach (渔获奖励 i23 in config.渔获奖励)
								{
									bool flag209 = i23.鱼类ID作为奖励组 == 手持物品ID;
									if (flag209)
									{
										foreach (Gofishing.Item1 it27 in i23.包含物品)
										{
											tsplayer.GiveItem(it27.NetID, it27.Stack * 背包第一格数量, it27.Perfix);
											it27 = null;
										}
										Gofishing.Item1[] array54 = null;
										args.Player.SendSuccessMessage(string.Format("你通过渔获回收，得到了 {1} 个 {0} 奖励", 手持物品ID, 背包第一格数量));
										return;
									}
									i23 = null;
								}
								渔获奖励[] array53 = null;
								args.Player.SendErrorMessage("腐竹没有设置奖励：" + 手持物品ID + "   如有需要请在配置文件设置!");
								return;
							}
							bool flag210 = "2438".Equals(手持物品ID);
							if (flag210)
							{
								args.Player.SendSuccessMessage(string.Format("已成功回收鱼类：[i:{0}]，回收数量：{1}，鉴定的重量为：{2} KG", 手持物品ID, 背包第一格数量, 随机重量 * 2));
								DbExt.Query(TShock.DB, "UPDATE 渔获统计 SET 粉水母 = 粉水母 + @0 WHERE 用户名 = @1", new object[] { 背包第一格数量, 用户名2 });
								DbExt.Query(TShock.DB, "UPDATE 渔获统计 SET 总量 = 总量 + @0 WHERE 用户名 = @1", new object[] { 背包第一格数量, 用户名2 });
								DbExt.Query(TShock.DB, "UPDATE 渔获统计 SET 竞赛分 = 竞赛分 + @0 WHERE 用户名 = @1", new object[]
								{
									背包第一格数量 * 2,
									用户名2
								});
								DbExt.Query(TShock.DB, "UPDATE 渔获统计 SET 总重量 = 总重量 + @0 WHERE 用户名 = @1", new object[]
								{
									随机重量 * 2,
									用户名2
								});
								bool flag211 = 重量 == 随机重量 * 2;
								if (flag211)
								{
									DbExt.Query(TShock.DB, "UPDATE 渔获鉴定 SET 数量 = 数量 + @0 WHERE (用户名 = @1 AND 鱼ID = @2) AND 重量 = @3", new object[]
									{
										1,
										用户名2,
										手持物品,
										随机重量 * 2
									});
								}
								else
								{
									bool flag212 = 重量 != 随机重量 * 2;
									if (flag212)
									{
										DbExt.Query(this._数据库, "INSERT INTO 渔获鉴定 (用户名, 鱼ID, 重量, 数量) VALUES (@0, @1, @2, @3);", new object[]
										{
											用户名2,
											手持物品,
											随机重量 * 2,
											背包第一格数量
										});
										bool flag213 = 最大重量 < 随机重量 * 2;
										if (flag213)
										{
											DbExt.Query(TShock.DB, "UPDATE 渔获最大 SET 最大重量 = @0 WHERE 用户名 = @1 AND 鱼ID = @2", new object[]
											{
												随机重量 * 2,
												用户名2,
												手持物品
											});
										}
									}
								}
								foreach (渔获奖励 i24 in config.渔获奖励)
								{
									bool flag214 = i24.鱼类ID作为奖励组 == 手持物品ID;
									if (flag214)
									{
										foreach (Gofishing.Item1 it28 in i24.包含物品)
										{
											tsplayer.GiveItem(it28.NetID, it28.Stack * 背包第一格数量, it28.Perfix);
											it28 = null;
										}
										Gofishing.Item1[] array56 = null;
										args.Player.SendSuccessMessage(string.Format("你通过渔获回收，得到了 {1} 个 {0} 奖励", 手持物品ID, 背包第一格数量));
										return;
									}
									i24 = null;
								}
								渔获奖励[] array55 = null;
								args.Player.SendErrorMessage("腐竹没有设置奖励：" + 手持物品ID + "   如有需要请在配置文件设置!");
								return;
							}
							bool flag215 = "4401".Equals(手持物品ID);
							if (flag215)
							{
								args.Player.SendSuccessMessage(string.Format("已成功回收鱼类：[i:{0}]，回收数量：{1}，鉴定的重量为：{2} KG", 手持物品ID, 背包第一格数量, 随机重量));
								DbExt.Query(TShock.DB, "UPDATE 渔获统计 SET 偏口鱼 = 偏口鱼 + @0 WHERE 用户名 = @1", new object[] { 背包第一格数量, 用户名2 });
								DbExt.Query(TShock.DB, "UPDATE 渔获统计 SET 总量 = 总量 + @0 WHERE 用户名 = @1", new object[] { 背包第一格数量, 用户名2 });
								DbExt.Query(TShock.DB, "UPDATE 渔获统计 SET 竞赛分 = 竞赛分 + @0 WHERE 用户名 = @1", new object[] { 背包第一格数量, 用户名2 });
								DbExt.Query(TShock.DB, "UPDATE 渔获统计 SET 总重量 = 总重量 + @0 WHERE 用户名 = @1", new object[] { 随机重量, 用户名2 });
								bool flag216 = 重量 == 随机重量;
								if (flag216)
								{
									DbExt.Query(TShock.DB, "UPDATE 渔获鉴定 SET 数量 = 数量 + @0 WHERE (用户名 = @1 AND 鱼ID = @2) AND 重量 = @3", new object[] { 1, 用户名2, 手持物品, 随机重量 });
								}
								else
								{
									bool flag217 = 重量 != 随机重量;
									if (flag217)
									{
										DbExt.Query(this._数据库, "INSERT INTO 渔获鉴定 (用户名, 鱼ID, 重量, 数量) VALUES (@0, @1, @2, @3);", new object[] { 用户名2, 手持物品, 随机重量, 背包第一格数量 });
										bool flag218 = 最大重量 < 随机重量;
										if (flag218)
										{
											DbExt.Query(TShock.DB, "UPDATE 渔获最大 SET 最大重量 = @0 WHERE 用户名 = @1 AND 鱼ID = @2", new object[] { 随机重量, 用户名2, 手持物品 });
										}
									}
								}
								foreach (渔获奖励 i25 in config.渔获奖励)
								{
									bool flag219 = i25.鱼类ID作为奖励组 == 手持物品ID;
									if (flag219)
									{
										foreach (Gofishing.Item1 it29 in i25.包含物品)
										{
											tsplayer.GiveItem(it29.NetID, it29.Stack * 背包第一格数量, it29.Perfix);
											it29 = null;
										}
										Gofishing.Item1[] array58 = null;
										args.Player.SendSuccessMessage(string.Format("你通过渔获回收，得到了 {1} 个 {0} 奖励", 手持物品ID, 背包第一格数量));
										return;
									}
									i25 = null;
								}
								渔获奖励[] array57 = null;
								args.Player.SendErrorMessage("腐竹没有设置奖励：" + 手持物品ID + "   如有需要请在配置文件设置!");
								return;
							}
							bool flag220 = "4402".Equals(手持物品ID);
							if (flag220)
							{
								args.Player.SendSuccessMessage(string.Format("已成功回收鱼类：[i:{0}]，回收数量：{1}，鉴定的重量为：{2} KG", 手持物品ID, 背包第一格数量, 随机重量));
								DbExt.Query(TShock.DB, "UPDATE 渔获统计 SET 岩龙虾 = 岩龙虾 + @0 WHERE 用户名 = @1", new object[] { 背包第一格数量, 用户名2 });
								DbExt.Query(TShock.DB, "UPDATE 渔获统计 SET 总量 = 总量 + @0 WHERE 用户名 = @1", new object[] { 背包第一格数量, 用户名2 });
								DbExt.Query(TShock.DB, "UPDATE 渔获统计 SET 竞赛分 = 竞赛分 + @0 WHERE 用户名 = @1", new object[] { 背包第一格数量, 用户名2 });
								DbExt.Query(TShock.DB, "UPDATE 渔获统计 SET 总重量 = 总重量 + @0 WHERE 用户名 = @1", new object[] { 随机重量, 用户名2 });
								bool flag221 = 重量 == 随机重量;
								if (flag221)
								{
									DbExt.Query(TShock.DB, "UPDATE 渔获鉴定 SET 数量 = 数量 + @0 WHERE (用户名 = @1 AND 鱼ID = @2) AND 重量 = @3", new object[] { 1, 用户名2, 手持物品, 随机重量 });
								}
								else
								{
									bool flag222 = 重量 != 随机重量;
									if (flag222)
									{
										DbExt.Query(this._数据库, "INSERT INTO 渔获鉴定 (用户名, 鱼ID, 重量, 数量) VALUES (@0, @1, @2, @3);", new object[] { 用户名2, 手持物品, 随机重量, 背包第一格数量 });
										bool flag223 = 最大重量 < 随机重量;
										if (flag223)
										{
											DbExt.Query(TShock.DB, "UPDATE 渔获最大 SET 最大重量 = @0 WHERE 用户名 = @1 AND 鱼ID = @2", new object[] { 随机重量, 用户名2, 手持物品 });
										}
									}
								}
								foreach (渔获奖励 i26 in config.渔获奖励)
								{
									bool flag224 = i26.鱼类ID作为奖励组 == 手持物品ID;
									if (flag224)
									{
										foreach (Gofishing.Item1 it30 in i26.包含物品)
										{
											tsplayer.GiveItem(it30.NetID, it30.Stack * 背包第一格数量, it30.Perfix);
											it30 = null;
										}
										Gofishing.Item1[] array60 = null;
										args.Player.SendSuccessMessage(string.Format("你通过渔获回收，得到了 {1} 个 {0} 奖励", 手持物品ID, 背包第一格数量));
										return;
									}
									i26 = null;
								}
								渔获奖励[] array59 = null;
								args.Player.SendErrorMessage("腐竹没有设置奖励：" + 手持物品ID + "   如有需要请在配置文件设置!");
								return;
							}
						}
						num35 = j;
					}
					args.Player.SendErrorMessage("显示栏第一格没放普通鱼，不能回收！");
					return;
				}
				IL_42E:
				try
				{
					using (QueryResult _表4 = DbExt.QueryReader(this._数据库, "SELECT * FROM 渔获统计 WHERE 用户名=@0", new object[] { args.Player.Name }))
					{
						while (!_表4.Read())
						{
							string _用户名3 = _表4.Get<string>("用户名");
							bool 存在数据库2 = args.Player.Name == _用户名3;
							bool flag225 = !存在数据库2;
							if (flag225)
							{
								TSPlayer _用户2 = args.Player;
								int 鲈鱼2 = 0;
								int 鳟鱼2 = 0;
								int 三文鱼2 = 0;
								int 大西洋鳕鱼2 = 0;
								int 金枪鱼2 = 0;
								int 红鲷鱼2 = 0;
								int 霓虹脂鲤2 = 0;
								int 装甲洞穴鱼2 = 0;
								int 雀鲷2 = 0;
								int 猩红虎鱼2 = 0;
								int 寒霜鲦鱼2 = 0;
								int 公主鱼2 = 0;
								int 金鲤鱼2 = 0;
								int 镜面鱼2 = 0;
								int 七彩矿鱼2 = 0;
								int 斑驳油鱼2 = 0;
								int 闪鳍锦鲤2 = 0;
								int 双鳍鳕鱼2 = 0;
								int 黑曜石鱼2 = 0;
								int 虾2 = 0;
								int 混沌鱼2 = 0;
								int 黑檀锦鲤2 = 0;
								int 血腥食人鱼2 = 0;
								int 臭味鱼2 = 0;
								int 蓝水母2 = 0;
								int 绿水母2 = 0;
								int 粉水母2 = 0;
								int 偏口鱼2 = 0;
								int 岩龙虾2 = 0;
								int 总量2 = 0;
								int 竞赛分3 = 0;
								int 竞赛排名2 = 0;
								int 总重量2 = 0;
								_用户2.SendSuccessMessage(_用户2.Name + "这是你第一次使用渔获回收，已自动帮您注册渔获数据。");
								DbExt.Query(this._数据库, "INSERT INTO 渔获统计 (用户名, 鲈鱼, 鳟鱼, 三文鱼, 大西洋鳕鱼, 金枪鱼, 红鲷鱼, 霓虹脂鲤, 装甲洞穴鱼, 雀鲷, 猩红虎鱼, 寒霜鲦鱼, 公主鱼, 金鲤鱼, 镜面鱼, 七彩矿鱼, 斑驳油鱼, 闪鳍锦鲤, 双鳍鳕鱼, 黑曜石鱼, 虾, 混沌鱼, 黑檀锦鲤, 血腥食人鱼, 臭味鱼, 蓝水母, 绿水母, 粉水母, 偏口鱼, 岩龙虾, 总量, 竞赛分, 竞赛排名, 总重量) VALUES (@0, @1, @2, @3, @4, @5, @6, @7, @8, @9, @10, @11, @12, @13, @14, @15, @16, @17, @18, @19, @20, @21, @22, @23, @24, @25, @26, @27, @28, @29, @30, @31, @32, @33);", new object[]
								{
									_用户2.Name, 鲈鱼2, 鳟鱼2, 三文鱼2, 大西洋鳕鱼2, 金枪鱼2, 红鲷鱼2, 霓虹脂鲤2, 装甲洞穴鱼2, 雀鲷2,
									猩红虎鱼2, 寒霜鲦鱼2, 公主鱼2, 金鲤鱼2, 镜面鱼2, 七彩矿鱼2, 斑驳油鱼2, 闪鳍锦鲤2, 双鳍鳕鱼2, 黑曜石鱼2,
									虾2, 混沌鱼2, 黑檀锦鲤2, 血腥食人鱼2, 臭味鱼2, 蓝水母2, 绿水母2, 粉水母2, 偏口鱼2, 岩龙虾2,
									总量2, 竞赛分3, 竞赛排名2, 总重量2
								});
								break;
							}
							_用户名3 = null;
						}
					}
					QueryResult _表4 = null;
				}
				catch (Exception ex39)
				{
					Exception ex3 = ex39;
					TShock.Log.ConsoleError(ex3.ToString());
				}
				TSPlayer tsplayer2 = args.Player;
				Config config2 = Config.GetConfig();
				string 用户名3 = args.Player.Name;
				int 手持物品2 = args.Player.SelectedItem.netID;
				string 手持物品名字2 = args.Player.SelectedItem.Name;
				string 手持物品ID2 = args.Player.SelectedItem.netID.ToString();
				int 背包第一格数量2 = args.Player.SelectedItem.stack;
				bool falg2 = "2290".Equals(手持物品ID2) || "2297".Equals(手持物品ID2) || "2298".Equals(手持物品ID2) || "2299".Equals(手持物品ID2) || "2300".Equals(手持物品ID2) || "2301".Equals(手持物品ID2) || "2302".Equals(手持物品ID2) || "2303".Equals(手持物品ID2) || "2304".Equals(手持物品ID2) || "2305".Equals(手持物品ID2) || "2306".Equals(手持物品ID2) || "2307".Equals(手持物品ID2) || "2308".Equals(手持物品ID2) || "2309".Equals(手持物品ID2) || "2310".Equals(手持物品ID2) || "2311".Equals(手持物品ID2) || "2312".Equals(手持物品ID2) || "2313".Equals(手持物品ID2) || "2315".Equals(手持物品ID2) || "2316".Equals(手持物品ID2) || "2317".Equals(手持物品ID2) || "2318".Equals(手持物品ID2) || "2319".Equals(手持物品ID2) || "2321".Equals(手持物品ID2) || "2436".Equals(手持物品ID2) || "2437".Equals(手持物品ID2) || "2438".Equals(手持物品ID2) || "4401".Equals(手持物品ID2) || "4402".Equals(手持物品ID2);
				bool flag226 = !falg2;
				if (flag226)
				{
					args.Player.SendErrorMessage("该物品不符合回收条件（普通鱼）：[i:" + 手持物品ID2 + "]");
					tsplayer2 = null;
					config2 = null;
					用户名3 = null;
					手持物品ID2 = null;
					goto IL_12A2E;
				}
				for (int k2 = 0; k2 < 1; k2 = num35 + 1)
				{
					bool flag227 = args.TPlayer.inventory[k2].type == 手持物品2;
					if (flag227)
					{
						背包第一格数量2 = args.TPlayer.inventory[k2].stack;
						int 新数量2 = args.TPlayer.inventory[k2].stack - 背包第一格数量2;
						this.PlayItemSet(args.Player.Index, k2, 手持物品2, 新数量2);
						bool flag228 = "2290".Equals(手持物品ID2);
						if (flag228)
						{
							args.Player.SendSuccessMessage(string.Format("已成功回收鱼类：[i:{0}]，回收数量：{1}", 手持物品ID2, 背包第一格数量2));
							DbExt.Query(TShock.DB, "UPDATE 渔获统计 SET 鲈鱼 = 鲈鱼 + @0 WHERE 用户名 = @1", new object[] { 背包第一格数量2, 用户名3 });
							DbExt.Query(TShock.DB, "UPDATE 渔获统计 SET 总量 = 总量 + @0 WHERE 用户名 = @1", new object[] { 背包第一格数量2, 用户名3 });
							DbExt.Query(TShock.DB, "UPDATE 渔获统计 SET 竞赛分 = 竞赛分 + @0 WHERE 用户名 = @1", new object[] { 背包第一格数量2, 用户名3 });
							foreach (渔获奖励 i27 in config2.渔获奖励)
							{
								bool flag229 = i27.鱼类ID作为奖励组 == 手持物品ID2;
								if (flag229)
								{
									foreach (Gofishing.Item1 it31 in i27.包含物品)
									{
										tsplayer2.GiveItem(it31.NetID, it31.Stack * 背包第一格数量2, it31.Perfix);
										it31 = null;
									}
									Gofishing.Item1[] array62 = null;
									args.Player.SendSuccessMessage(string.Format("你通过渔获回收，得到了 {1} 个 {0} 奖励", 手持物品ID2, 背包第一格数量2));
									return;
								}
								i27 = null;
							}
							渔获奖励[] array61 = null;
							args.Player.SendErrorMessage("腐竹没有设置奖励：" + 手持物品ID2 + "   如有需要请在配置文件设置!");
							return;
						}
						bool flag230 = "2297".Equals(手持物品ID2);
						if (flag230)
						{
							args.Player.SendSuccessMessage(string.Format("已成功回收鱼类：[i:{0}]，回收数量：{1}", 手持物品ID2, 背包第一格数量2));
							DbExt.Query(TShock.DB, "UPDATE 渔获统计 SET 鳟鱼 = 鳟鱼 + @0 WHERE 用户名 = @1", new object[] { 背包第一格数量2, 用户名3 });
							DbExt.Query(TShock.DB, "UPDATE 渔获统计 SET 总量 = 总量 + @0 WHERE 用户名 = @1", new object[] { 背包第一格数量2, 用户名3 });
							DbExt.Query(TShock.DB, "UPDATE 渔获统计 SET 竞赛分 = 竞赛分 + @0 WHERE 用户名 = @1", new object[] { 背包第一格数量2, 用户名3 });
							foreach (渔获奖励 i28 in config2.渔获奖励)
							{
								bool flag231 = i28.鱼类ID作为奖励组 == 手持物品ID2;
								if (flag231)
								{
									foreach (Gofishing.Item1 it32 in i28.包含物品)
									{
										tsplayer2.GiveItem(it32.NetID, it32.Stack * 背包第一格数量2, it32.Perfix);
										it32 = null;
									}
									Gofishing.Item1[] array64 = null;
									args.Player.SendSuccessMessage(string.Format("你通过渔获回收，得到了 {1} 个 {0} 奖励", 手持物品ID2, 背包第一格数量2));
									return;
								}
								i28 = null;
							}
							渔获奖励[] array63 = null;
							args.Player.SendErrorMessage("腐竹没有设置奖励：" + 手持物品ID2 + "   如有需要请在配置文件设置!");
							return;
						}
						bool flag232 = "2298".Equals(手持物品ID2);
						if (flag232)
						{
							args.Player.SendSuccessMessage(string.Format("已成功回收鱼类：[i:{0}]，回收数量：{1}", 手持物品ID2, 背包第一格数量2));
							DbExt.Query(TShock.DB, "UPDATE 渔获统计 SET 三文鱼 = 三文鱼 + @0 WHERE 用户名 = @1", new object[] { 背包第一格数量2, 用户名3 });
							DbExt.Query(TShock.DB, "UPDATE 渔获统计 SET 总量 = 总量 + @0 WHERE 用户名 = @1", new object[] { 背包第一格数量2, 用户名3 });
							DbExt.Query(TShock.DB, "UPDATE 渔获统计 SET 竞赛分 = 竞赛分 + @0 WHERE 用户名 = @1", new object[] { 背包第一格数量2, 用户名3 });
							foreach (渔获奖励 i29 in config2.渔获奖励)
							{
								bool flag233 = i29.鱼类ID作为奖励组 == 手持物品ID2;
								if (flag233)
								{
									foreach (Gofishing.Item1 it33 in i29.包含物品)
									{
										tsplayer2.GiveItem(it33.NetID, it33.Stack * 背包第一格数量2, it33.Perfix);
										it33 = null;
									}
									Gofishing.Item1[] array66 = null;
									args.Player.SendSuccessMessage(string.Format("你通过渔获回收，得到了 {1} 个 {0} 奖励", 手持物品ID2, 背包第一格数量2));
									return;
								}
								i29 = null;
							}
							渔获奖励[] array65 = null;
							args.Player.SendErrorMessage("腐竹没有设置奖励：" + 手持物品ID2 + "   如有需要请在配置文件设置!");
							return;
						}
						bool flag234 = "2299".Equals(手持物品ID2);
						if (flag234)
						{
							args.Player.SendSuccessMessage(string.Format("已成功回收鱼类：[i:{0}]，回收数量：{1}", 手持物品ID2, 背包第一格数量2));
							DbExt.Query(TShock.DB, "UPDATE 渔获统计 SET 大西洋鳕鱼 = 大西洋鳕鱼 + @0 WHERE 用户名 = @1", new object[] { 背包第一格数量2, 用户名3 });
							DbExt.Query(TShock.DB, "UPDATE 渔获统计 SET 总量 = 总量 + @0 WHERE 用户名 = @1", new object[] { 背包第一格数量2, 用户名3 });
							DbExt.Query(TShock.DB, "UPDATE 渔获统计 SET 竞赛分 = 竞赛分 + @0 WHERE 用户名 = @1", new object[] { 背包第一格数量2, 用户名3 });
							foreach (渔获奖励 i30 in config2.渔获奖励)
							{
								bool flag235 = i30.鱼类ID作为奖励组 == 手持物品ID2;
								if (flag235)
								{
									foreach (Gofishing.Item1 it34 in i30.包含物品)
									{
										tsplayer2.GiveItem(it34.NetID, it34.Stack * 背包第一格数量2, it34.Perfix);
										it34 = null;
									}
									Gofishing.Item1[] array68 = null;
									args.Player.SendSuccessMessage(string.Format("你通过渔获回收，得到了 {1} 个 {0} 奖励", 手持物品ID2, 背包第一格数量2));
									return;
								}
								i30 = null;
							}
							渔获奖励[] array67 = null;
							args.Player.SendErrorMessage("腐竹没有设置奖励：" + 手持物品ID2 + "   如有需要请在配置文件设置!");
							return;
						}
						bool flag236 = "2300".Equals(手持物品ID2);
						if (flag236)
						{
							args.Player.SendSuccessMessage(string.Format("已成功回收鱼类：[i:{0}]，回收数量：{1}", 手持物品ID2, 背包第一格数量2));
							DbExt.Query(TShock.DB, "UPDATE 渔获统计 SET 金枪鱼 = 金枪鱼 + @0 WHERE 用户名 = @1", new object[] { 背包第一格数量2, 用户名3 });
							DbExt.Query(TShock.DB, "UPDATE 渔获统计 SET 总量 = 总量 + @0 WHERE 用户名 = @1", new object[] { 背包第一格数量2, 用户名3 });
							DbExt.Query(TShock.DB, "UPDATE 渔获统计 SET 竞赛分 = 竞赛分 + @0 WHERE 用户名 = @1", new object[] { 背包第一格数量2, 用户名3 });
							foreach (渔获奖励 i31 in config2.渔获奖励)
							{
								bool flag237 = i31.鱼类ID作为奖励组 == 手持物品ID2;
								if (flag237)
								{
									foreach (Gofishing.Item1 it35 in i31.包含物品)
									{
										tsplayer2.GiveItem(it35.NetID, it35.Stack * 背包第一格数量2, it35.Perfix);
										it35 = null;
									}
									Gofishing.Item1[] array70 = null;
									args.Player.SendSuccessMessage(string.Format("你通过渔获回收，得到了 {1} 个 {0} 奖励", 手持物品ID2, 背包第一格数量2));
									return;
								}
								i31 = null;
							}
							渔获奖励[] array69 = null;
							args.Player.SendErrorMessage("腐竹没有设置奖励：" + 手持物品ID2 + "   如有需要请在配置文件设置!");
							return;
						}
						bool flag238 = "2301".Equals(手持物品ID2);
						if (flag238)
						{
							args.Player.SendSuccessMessage(string.Format("已成功回收鱼类：[i:{0}]，回收数量：{1}", 手持物品ID2, 背包第一格数量2));
							DbExt.Query(TShock.DB, "UPDATE 渔获统计 SET 红鲷鱼= 红鲷鱼 + @0 WHERE 用户名 = @1", new object[] { 背包第一格数量2, 用户名3 });
							DbExt.Query(TShock.DB, "UPDATE 渔获统计 SET 总量 = 总量 + @0 WHERE 用户名 = @1", new object[] { 背包第一格数量2, 用户名3 });
							DbExt.Query(TShock.DB, "UPDATE 渔获统计 SET 竞赛分 = 竞赛分 + @0 WHERE 用户名 = @1", new object[] { 背包第一格数量2, 用户名3 });
							foreach (渔获奖励 i32 in config2.渔获奖励)
							{
								bool flag239 = i32.鱼类ID作为奖励组 == 手持物品ID2;
								if (flag239)
								{
									foreach (Gofishing.Item1 it36 in i32.包含物品)
									{
										tsplayer2.GiveItem(it36.NetID, it36.Stack * 背包第一格数量2, it36.Perfix);
										it36 = null;
									}
									Gofishing.Item1[] array72 = null;
									args.Player.SendSuccessMessage(string.Format("你通过渔获回收，得到了 {1} 个 {0} 奖励", 手持物品ID2, 背包第一格数量2));
									return;
								}
								i32 = null;
							}
							渔获奖励[] array71 = null;
							args.Player.SendErrorMessage("腐竹没有设置奖励：" + 手持物品ID2 + "   如有需要请在配置文件设置!");
							return;
						}
						bool flag240 = "2302".Equals(手持物品ID2);
						if (flag240)
						{
							args.Player.SendSuccessMessage(string.Format("已成功回收鱼类：[i:{0}]，回收数量：{1}", 手持物品ID2, 背包第一格数量2));
							DbExt.Query(TShock.DB, "UPDATE 渔获统计 SET 霓虹脂鲤 = 霓虹脂鲤 + @0 WHERE 用户名 = @1", new object[] { 背包第一格数量2, 用户名3 });
							DbExt.Query(TShock.DB, "UPDATE 渔获统计 SET 总量 = 总量 + @0 WHERE 用户名 = @1", new object[] { 背包第一格数量2, 用户名3 });
							DbExt.Query(TShock.DB, "UPDATE 渔获统计 SET 竞赛分 = 竞赛分 + @0 WHERE 用户名 = @1", new object[] { 背包第一格数量2, 用户名3 });
							foreach (渔获奖励 i33 in config2.渔获奖励)
							{
								bool flag241 = i33.鱼类ID作为奖励组 == 手持物品ID2;
								if (flag241)
								{
									foreach (Gofishing.Item1 it37 in i33.包含物品)
									{
										tsplayer2.GiveItem(it37.NetID, it37.Stack * 背包第一格数量2, it37.Perfix);
										it37 = null;
									}
									Gofishing.Item1[] array74 = null;
									args.Player.SendSuccessMessage(string.Format("你通过渔获回收，得到了 {1} 个 {0} 奖励", 手持物品ID2, 背包第一格数量2));
									return;
								}
								i33 = null;
							}
							渔获奖励[] array73 = null;
							args.Player.SendErrorMessage("腐竹没有设置奖励：" + 手持物品ID2 + "   如有需要请在配置文件设置!");
							return;
						}
						bool flag242 = "2303".Equals(手持物品ID2);
						if (flag242)
						{
							args.Player.SendSuccessMessage(string.Format("已成功回收鱼类：[i:{0}]，回收数量：{1}", 手持物品ID2, 背包第一格数量2));
							DbExt.Query(TShock.DB, "UPDATE 渔获统计 SET 装甲洞穴鱼 = 装甲洞穴鱼 + @0 WHERE 用户名 = @1", new object[] { 背包第一格数量2, 用户名3 });
							DbExt.Query(TShock.DB, "UPDATE 渔获统计 SET 总量 = 总量 + @0 WHERE 用户名 = @1", new object[] { 背包第一格数量2, 用户名3 });
							DbExt.Query(TShock.DB, "UPDATE 渔获统计 SET 竞赛分 = 竞赛分 + @0 WHERE 用户名 = @1", new object[]
							{
								背包第一格数量2 * 2,
								用户名3
							});
							foreach (渔获奖励 i34 in config2.渔获奖励)
							{
								bool flag243 = i34.鱼类ID作为奖励组 == 手持物品ID2;
								if (flag243)
								{
									foreach (Gofishing.Item1 it38 in i34.包含物品)
									{
										tsplayer2.GiveItem(it38.NetID, it38.Stack * 背包第一格数量2, it38.Perfix);
										it38 = null;
									}
									Gofishing.Item1[] array76 = null;
									args.Player.SendSuccessMessage(string.Format("你通过渔获回收，得到了 {1} 个 {0} 奖励", 手持物品ID2, 背包第一格数量2));
									return;
								}
								i34 = null;
							}
							渔获奖励[] array75 = null;
							args.Player.SendErrorMessage("腐竹没有设置奖励：" + 手持物品ID2 + "   如有需要请在配置文件设置!");
							return;
						}
						bool flag244 = "2304".Equals(手持物品ID2);
						if (flag244)
						{
							args.Player.SendSuccessMessage(string.Format("已成功回收鱼类：[i:{0}]，回收数量：{1}", 手持物品ID2, 背包第一格数量2));
							DbExt.Query(TShock.DB, "UPDATE 渔获统计 SET 雀鲷 = 雀鲷 + @0 WHERE 用户名 = @1", new object[] { 背包第一格数量2, 用户名3 });
							DbExt.Query(TShock.DB, "UPDATE 渔获统计 SET 总量 = 总量 + @0 WHERE 用户名 = @1", new object[] { 背包第一格数量2, 用户名3 });
							DbExt.Query(TShock.DB, "UPDATE 渔获统计 SET 竞赛分 = 竞赛分 + @0 WHERE 用户名 = @1", new object[]
							{
								背包第一格数量2 * 2,
								用户名3
							});
							foreach (渔获奖励 i35 in config2.渔获奖励)
							{
								bool flag245 = i35.鱼类ID作为奖励组 == 手持物品ID2;
								if (flag245)
								{
									foreach (Gofishing.Item1 it39 in i35.包含物品)
									{
										tsplayer2.GiveItem(it39.NetID, it39.Stack * 背包第一格数量2, it39.Perfix);
										it39 = null;
									}
									Gofishing.Item1[] array78 = null;
									args.Player.SendSuccessMessage(string.Format("你通过渔获回收，得到了 {1} 个 {0} 奖励", 手持物品ID2, 背包第一格数量2));
									return;
								}
								i35 = null;
							}
							渔获奖励[] array77 = null;
							args.Player.SendErrorMessage("腐竹没有设置奖励：" + 手持物品ID2 + "   如有需要请在配置文件设置!");
							return;
						}
						bool flag246 = "2305".Equals(手持物品ID2);
						if (flag246)
						{
							args.Player.SendSuccessMessage(string.Format("已成功回收鱼类：[i:{0}]，回收数量：{1}", 手持物品ID2, 背包第一格数量2));
							DbExt.Query(TShock.DB, "UPDATE 渔获统计 SET 猩红虎鱼 = 猩红虎鱼 + @0 WHERE 用户名 = @1", new object[] { 背包第一格数量2, 用户名3 });
							DbExt.Query(TShock.DB, "UPDATE 渔获统计 SET 总量 = 总量 + @0 WHERE 用户名 = @1", new object[] { 背包第一格数量2, 用户名3 });
							DbExt.Query(TShock.DB, "UPDATE 渔获统计 SET 竞赛分 = 竞赛分 + @0 WHERE 用户名 = @1", new object[]
							{
								背包第一格数量2 * 2,
								用户名3
							});
							foreach (渔获奖励 i36 in config2.渔获奖励)
							{
								bool flag247 = i36.鱼类ID作为奖励组 == 手持物品ID2;
								if (flag247)
								{
									foreach (Gofishing.Item1 it40 in i36.包含物品)
									{
										tsplayer2.GiveItem(it40.NetID, it40.Stack * 背包第一格数量2, it40.Perfix);
										it40 = null;
									}
									Gofishing.Item1[] array80 = null;
									args.Player.SendSuccessMessage(string.Format("你通过渔获回收，得到了 {1} 个 {0} 奖励", 手持物品ID2, 背包第一格数量2));
									return;
								}
								i36 = null;
							}
							渔获奖励[] array79 = null;
							args.Player.SendErrorMessage("腐竹没有设置奖励：" + 手持物品ID2 + "   如有需要请在配置文件设置!");
							return;
						}
						bool flag248 = "2306".Equals(手持物品ID2);
						if (flag248)
						{
							args.Player.SendSuccessMessage(string.Format("已成功回收鱼类：[i:{0}]，回收数量：{1}", 手持物品ID2, 背包第一格数量2));
							DbExt.Query(TShock.DB, "UPDATE 渔获统计 SET 寒霜鲦鱼 = 寒霜鲦鱼 + @0 WHERE 用户名 = @1", new object[] { 背包第一格数量2, 用户名3 });
							DbExt.Query(TShock.DB, "UPDATE 渔获统计 SET 总量 = 总量 + @0 WHERE 用户名 = @1", new object[] { 背包第一格数量2, 用户名3 });
							DbExt.Query(TShock.DB, "UPDATE 渔获统计 SET 竞赛分 = 竞赛分 + @0 WHERE 用户名 = @1", new object[]
							{
								背包第一格数量2 * 2,
								用户名3
							});
							foreach (渔获奖励 i37 in config2.渔获奖励)
							{
								bool flag249 = i37.鱼类ID作为奖励组 == 手持物品ID2;
								if (flag249)
								{
									foreach (Gofishing.Item1 it41 in i37.包含物品)
									{
										tsplayer2.GiveItem(it41.NetID, it41.Stack * 背包第一格数量2, it41.Perfix);
										it41 = null;
									}
									Gofishing.Item1[] array82 = null;
									args.Player.SendSuccessMessage(string.Format("你通过渔获回收，得到了 {1} 个 {0} 奖励", 手持物品ID2, 背包第一格数量2));
									return;
								}
								i37 = null;
							}
							渔获奖励[] array81 = null;
							args.Player.SendErrorMessage("腐竹没有设置奖励：" + 手持物品ID2 + "   如有需要请在配置文件设置!");
							return;
						}
						bool flag250 = "2307".Equals(手持物品ID2);
						if (flag250)
						{
							args.Player.SendSuccessMessage(string.Format("已成功回收鱼类：[i:{0}]，回收数量：{1}", 手持物品ID2, 背包第一格数量2));
							DbExt.Query(TShock.DB, "UPDATE 渔获统计 SET 公主鱼 = 公主鱼 + @0 WHERE 用户名 = @1", new object[] { 背包第一格数量2, 用户名3 });
							DbExt.Query(TShock.DB, "UPDATE 渔获统计 SET 总量 = 总量 + @0 WHERE 用户名 = @1", new object[] { 背包第一格数量2, 用户名3 });
							DbExt.Query(TShock.DB, "UPDATE 渔获统计 SET 竞赛分 = 竞赛分 + @0 WHERE 用户名 = @1", new object[]
							{
								背包第一格数量2 * 3,
								用户名3
							});
							foreach (渔获奖励 i38 in config2.渔获奖励)
							{
								bool flag251 = i38.鱼类ID作为奖励组 == 手持物品ID2;
								if (flag251)
								{
									foreach (Gofishing.Item1 it42 in i38.包含物品)
									{
										tsplayer2.GiveItem(it42.NetID, it42.Stack * 背包第一格数量2, it42.Perfix);
										it42 = null;
									}
									Gofishing.Item1[] array84 = null;
									args.Player.SendSuccessMessage(string.Format("你通过渔获回收，得到了 {1} 个 {0} 奖励", 手持物品ID2, 背包第一格数量2));
									return;
								}
								i38 = null;
							}
							渔获奖励[] array83 = null;
							args.Player.SendErrorMessage("腐竹没有设置奖励：" + 手持物品ID2 + "   如有需要请在配置文件设置!");
							return;
						}
						bool flag252 = "2308".Equals(手持物品ID2);
						if (flag252)
						{
							args.Player.SendSuccessMessage(string.Format("已成功回收鱼类：[i:{0}]，回收数量：{1}", 手持物品ID2, 背包第一格数量2));
							DbExt.Query(TShock.DB, "UPDATE 渔获统计 SET 金鲤鱼 = 金鲤鱼 + @0 WHERE 用户名 = @1", new object[] { 背包第一格数量2, 用户名3 });
							DbExt.Query(TShock.DB, "UPDATE 渔获统计 SET 总量 = 总量 + @0 WHERE 用户名 = @1", new object[] { 背包第一格数量2, 用户名3 });
							DbExt.Query(TShock.DB, "UPDATE 渔获统计 SET 竞赛分 = 竞赛分 + @0 WHERE 用户名 = @1", new object[]
							{
								背包第一格数量2 * 5,
								用户名3
							});
							foreach (渔获奖励 i39 in config2.渔获奖励)
							{
								bool flag253 = i39.鱼类ID作为奖励组 == 手持物品ID2;
								if (flag253)
								{
									foreach (Gofishing.Item1 it43 in i39.包含物品)
									{
										tsplayer2.GiveItem(it43.NetID, it43.Stack * 背包第一格数量2, it43.Perfix);
										it43 = null;
									}
									Gofishing.Item1[] array86 = null;
									args.Player.SendSuccessMessage(string.Format("你通过渔获回收，得到了 {1} 个 {0} 奖励", 手持物品ID2, 背包第一格数量2));
									return;
								}
								i39 = null;
							}
							渔获奖励[] array85 = null;
							args.Player.SendErrorMessage("腐竹没有设置奖励：" + 手持物品ID2 + "   如有需要请在配置文件设置!");
							return;
						}
						bool flag254 = "2309".Equals(手持物品ID2);
						if (flag254)
						{
							args.Player.SendSuccessMessage(string.Format("已成功回收鱼类：[i:{0}]，回收数量：{1}", 手持物品ID2, 背包第一格数量2));
							DbExt.Query(TShock.DB, "UPDATE 渔获统计 SET 镜面鱼 = 镜面鱼 + @0 WHERE 用户名 = @1", new object[] { 背包第一格数量2, 用户名3 });
							DbExt.Query(TShock.DB, "UPDATE 渔获统计 SET 总量 = 总量 + @0 WHERE 用户名 = @1", new object[] { 背包第一格数量2, 用户名3 });
							DbExt.Query(TShock.DB, "UPDATE 渔获统计 SET 竞赛分 = 竞赛分 + @0 WHERE 用户名 = @1", new object[]
							{
								背包第一格数量2 * 2,
								用户名3
							});
							foreach (渔获奖励 i40 in config2.渔获奖励)
							{
								bool flag255 = i40.鱼类ID作为奖励组 == 手持物品ID2;
								if (flag255)
								{
									foreach (Gofishing.Item1 it44 in i40.包含物品)
									{
										tsplayer2.GiveItem(it44.NetID, it44.Stack * 背包第一格数量2, it44.Perfix);
										it44 = null;
									}
									Gofishing.Item1[] array88 = null;
									args.Player.SendSuccessMessage(string.Format("你通过渔获回收，得到了 {1} 个 {0} 奖励", 手持物品ID2, 背包第一格数量2));
									return;
								}
								i40 = null;
							}
							渔获奖励[] array87 = null;
							args.Player.SendErrorMessage("腐竹没有设置奖励：" + 手持物品ID2 + "   如有需要请在配置文件设置!");
							return;
						}
						bool flag256 = "2310".Equals(手持物品ID2);
						if (flag256)
						{
							args.Player.SendSuccessMessage(string.Format("已成功回收鱼类：[i:{0}]，回收数量：{1}", 手持物品ID2, 背包第一格数量2));
							DbExt.Query(TShock.DB, "UPDATE 渔获统计 SET 七彩矿鱼 = 七彩矿鱼 + @0 WHERE 用户名 = @1", new object[] { 背包第一格数量2, 用户名3 });
							DbExt.Query(TShock.DB, "UPDATE 渔获统计 SET 总量 = 总量 + @0 WHERE 用户名 = @1", new object[] { 背包第一格数量2, 用户名3 });
							DbExt.Query(TShock.DB, "UPDATE 渔获统计 SET 竞赛分 = 竞赛分 + @0 WHERE 用户名 = @1", new object[]
							{
								背包第一格数量2 * 4,
								用户名3
							});
							foreach (渔获奖励 i41 in config2.渔获奖励)
							{
								bool flag257 = i41.鱼类ID作为奖励组 == 手持物品ID2;
								if (flag257)
								{
									foreach (Gofishing.Item1 it45 in i41.包含物品)
									{
										tsplayer2.GiveItem(it45.NetID, it45.Stack * 背包第一格数量2, it45.Perfix);
										it45 = null;
									}
									Gofishing.Item1[] array90 = null;
									args.Player.SendSuccessMessage(string.Format("你通过渔获回收，得到了 {1} 个 {0} 奖励", 手持物品ID2, 背包第一格数量2));
									return;
								}
								i41 = null;
							}
							渔获奖励[] array89 = null;
							args.Player.SendErrorMessage("腐竹没有设置奖励：" + 手持物品ID2 + "   如有需要请在配置文件设置!");
							return;
						}
						bool flag258 = "2311".Equals(手持物品ID2);
						if (flag258)
						{
							args.Player.SendSuccessMessage(string.Format("已成功回收鱼类：[i:{0}]，回收数量：{1}", 手持物品ID2, 背包第一格数量2));
							DbExt.Query(TShock.DB, "UPDATE 渔获统计 SET 斑驳油鱼 = 斑驳油鱼 + @0 WHERE 用户名 = @1", new object[] { 背包第一格数量2, 用户名3 });
							DbExt.Query(TShock.DB, "UPDATE 渔获统计 SET 总量 = 总量 + @0 WHERE 用户名 = @1", new object[] { 背包第一格数量2, 用户名3 });
							DbExt.Query(TShock.DB, "UPDATE 渔获统计 SET 竞赛分 = 竞赛分 + @0 WHERE 用户名 = @1", new object[]
							{
								背包第一格数量2 * 2,
								用户名3
							});
							foreach (渔获奖励 i42 in config2.渔获奖励)
							{
								bool flag259 = i42.鱼类ID作为奖励组 == 手持物品ID2;
								if (flag259)
								{
									foreach (Gofishing.Item1 it46 in i42.包含物品)
									{
										tsplayer2.GiveItem(it46.NetID, it46.Stack * 背包第一格数量2, it46.Perfix);
										it46 = null;
									}
									Gofishing.Item1[] array92 = null;
									args.Player.SendSuccessMessage(string.Format("你通过渔获回收，得到了 {1} 个 {0} 奖励", 手持物品ID2, 背包第一格数量2));
									return;
								}
								i42 = null;
							}
							渔获奖励[] array91 = null;
							args.Player.SendErrorMessage("腐竹没有设置奖励：" + 手持物品ID2 + "   如有需要请在配置文件设置!");
							return;
						}
						bool flag260 = "2312".Equals(手持物品ID2);
						if (flag260)
						{
							args.Player.SendSuccessMessage(string.Format("已成功回收鱼类：[i:{0}]，回收数量：{1}", 手持物品ID2, 背包第一格数量2));
							DbExt.Query(TShock.DB, "UPDATE 渔获统计 SET 闪鳍锦鲤 = 闪鳍锦鲤 + @0 WHERE 用户名 = @1", new object[] { 背包第一格数量2, 用户名3 });
							DbExt.Query(TShock.DB, "UPDATE 渔获统计 SET 总量 = 总量 + @0 WHERE 用户名 = @1", new object[] { 背包第一格数量2, 用户名3 });
							DbExt.Query(TShock.DB, "UPDATE 渔获统计 SET 竞赛分 = 竞赛分 + @0 WHERE 用户名 = @1", new object[]
							{
								背包第一格数量2 * 3,
								用户名3
							});
							foreach (渔获奖励 i43 in config2.渔获奖励)
							{
								bool flag261 = i43.鱼类ID作为奖励组 == 手持物品ID2;
								if (flag261)
								{
									foreach (Gofishing.Item1 it47 in i43.包含物品)
									{
										tsplayer2.GiveItem(it47.NetID, it47.Stack * 背包第一格数量2, it47.Perfix);
										it47 = null;
									}
									Gofishing.Item1[] array94 = null;
									args.Player.SendSuccessMessage(string.Format("你通过渔获回收，得到了 {1} 个 {0} 奖励", 手持物品ID2, 背包第一格数量2));
									return;
								}
								i43 = null;
							}
							渔获奖励[] array93 = null;
							args.Player.SendErrorMessage("腐竹没有设置奖励：" + 手持物品ID2 + "   如有需要请在配置文件设置!");
							return;
						}
						bool flag262 = "2313".Equals(手持物品ID2);
						if (flag262)
						{
							args.Player.SendSuccessMessage(string.Format("已成功回收鱼类：[i:{0}]，回收数量：{1}", 手持物品ID2, 背包第一格数量2));
							DbExt.Query(TShock.DB, "UPDATE 渔获统计 SET 双鳍鳕鱼 = 双鳍鳕鱼 + @0 WHERE 用户名 = @1", new object[] { 背包第一格数量2, 用户名3 });
							DbExt.Query(TShock.DB, "UPDATE 渔获统计 SET 总量 = 总量 + @0 WHERE 用户名 = @1", new object[] { 背包第一格数量2, 用户名3 });
							DbExt.Query(TShock.DB, "UPDATE 渔获统计 SET 竞赛分 = 竞赛分 + @0 WHERE 用户名 = @1", new object[]
							{
								背包第一格数量2 * 2,
								用户名3
							});
							foreach (渔获奖励 i44 in config2.渔获奖励)
							{
								bool flag263 = i44.鱼类ID作为奖励组 == 手持物品ID2;
								if (flag263)
								{
									foreach (Gofishing.Item1 it48 in i44.包含物品)
									{
										tsplayer2.GiveItem(it48.NetID, it48.Stack * 背包第一格数量2, it48.Perfix);
										it48 = null;
									}
									Gofishing.Item1[] array96 = null;
									args.Player.SendSuccessMessage(string.Format("你通过渔获回收，得到了 {1} 个 {0} 奖励", 手持物品ID2, 背包第一格数量2));
									return;
								}
								i44 = null;
							}
							渔获奖励[] array95 = null;
							args.Player.SendErrorMessage("腐竹没有设置奖励：" + 手持物品ID2 + "   如有需要请在配置文件设置!");
							return;
						}
						bool flag264 = "2315".Equals(手持物品ID2);
						if (flag264)
						{
							args.Player.SendSuccessMessage(string.Format("已成功回收鱼类：[i:{0}]，回收数量：{1}", 手持物品ID2, 背包第一格数量2));
							DbExt.Query(TShock.DB, "UPDATE 渔获统计 SET 黑曜石鱼 = 黑曜石鱼 + @0 WHERE 用户名 = @1", new object[] { 背包第一格数量2, 用户名3 });
							DbExt.Query(TShock.DB, "UPDATE 渔获统计 SET 总量 = 总量 + @0 WHERE 用户名 = @1", new object[] { 背包第一格数量2, 用户名3 });
							DbExt.Query(TShock.DB, "UPDATE 渔获统计 SET 竞赛分 = 竞赛分 + @0 WHERE 用户名 = @1", new object[]
							{
								背包第一格数量2 * 3,
								用户名3
							});
							foreach (渔获奖励 i45 in config2.渔获奖励)
							{
								bool flag265 = i45.鱼类ID作为奖励组 == 手持物品ID2;
								if (flag265)
								{
									foreach (Gofishing.Item1 it49 in i45.包含物品)
									{
										tsplayer2.GiveItem(it49.NetID, it49.Stack * 背包第一格数量2, it49.Perfix);
										it49 = null;
									}
									Gofishing.Item1[] array98 = null;
									args.Player.SendSuccessMessage(string.Format("你通过渔获回收，得到了 {1} 个 {0} 奖励", 手持物品ID2, 背包第一格数量2));
									return;
								}
								i45 = null;
							}
							渔获奖励[] array97 = null;
							args.Player.SendErrorMessage("腐竹没有设置奖励：" + 手持物品ID2 + "   如有需要请在配置文件设置!");
							return;
						}
						bool flag266 = "2316".Equals(手持物品ID2);
						if (flag266)
						{
							args.Player.SendSuccessMessage(string.Format("已成功回收鱼类：[i:{0}]，回收数量：{1}", 手持物品ID2, 背包第一格数量2));
							DbExt.Query(TShock.DB, "UPDATE 渔获统计 SET 虾 = 虾 + @0 WHERE 用户名 = @1", new object[] { 背包第一格数量2, 用户名3 });
							DbExt.Query(TShock.DB, "UPDATE 渔获统计 SET 总量 = 总量 + @0 WHERE 用户名 = @1", new object[] { 背包第一格数量2, 用户名3 });
							DbExt.Query(TShock.DB, "UPDATE 渔获统计 SET 竞赛分 = 竞赛分 + @0 WHERE 用户名 = @1", new object[] { 背包第一格数量2, 用户名3 });
							foreach (渔获奖励 i46 in config2.渔获奖励)
							{
								bool flag267 = i46.鱼类ID作为奖励组 == 手持物品ID2;
								if (flag267)
								{
									foreach (Gofishing.Item1 it50 in i46.包含物品)
									{
										tsplayer2.GiveItem(it50.NetID, it50.Stack * 背包第一格数量2, it50.Perfix);
										it50 = null;
									}
									Gofishing.Item1[] array100 = null;
									args.Player.SendSuccessMessage(string.Format("你通过渔获回收，得到了 {1} 个 {0} 奖励", 手持物品ID2, 背包第一格数量2));
									return;
								}
								i46 = null;
							}
							渔获奖励[] array99 = null;
							args.Player.SendErrorMessage("腐竹没有设置奖励：" + 手持物品ID2 + "   如有需要请在配置文件设置!");
							return;
						}
						bool flag268 = "2317".Equals(手持物品ID2);
						if (flag268)
						{
							args.Player.SendSuccessMessage(string.Format("已成功回收鱼类：[i:{0}]，回收数量：{1}", 手持物品ID2, 背包第一格数量2));
							DbExt.Query(TShock.DB, "UPDATE 渔获统计 SET 混沌鱼 = 混沌鱼 + @0 WHERE 用户名 = @1", new object[] { 背包第一格数量2, 用户名3 });
							DbExt.Query(TShock.DB, "UPDATE 渔获统计 SET 总量 = 总量 + @0 WHERE 用户名 = @1", new object[] { 背包第一格数量2, 用户名3 });
							DbExt.Query(TShock.DB, "UPDATE 渔获统计 SET 竞赛分 = 竞赛分 + @0 WHERE 用户名 = @1", new object[]
							{
								背包第一格数量2 * 5,
								用户名3
							});
							foreach (渔获奖励 i47 in config2.渔获奖励)
							{
								bool flag269 = i47.鱼类ID作为奖励组 == 手持物品ID2;
								if (flag269)
								{
									foreach (Gofishing.Item1 it51 in i47.包含物品)
									{
										tsplayer2.GiveItem(it51.NetID, it51.Stack * 背包第一格数量2, it51.Perfix);
										it51 = null;
									}
									Gofishing.Item1[] array102 = null;
									args.Player.SendSuccessMessage(string.Format("你通过渔获回收，得到了 {1} 个 {0} 奖励", 手持物品ID2, 背包第一格数量2));
									return;
								}
								i47 = null;
							}
							渔获奖励[] array101 = null;
							args.Player.SendErrorMessage("腐竹没有设置奖励：" + 手持物品ID2 + "   如有需要请在配置文件设置!");
							return;
						}
						bool flag270 = "2318".Equals(手持物品ID2);
						if (flag270)
						{
							args.Player.SendSuccessMessage(string.Format("已成功回收鱼类：[i:{0}]，回收数量：{1}", 手持物品ID2, 背包第一格数量2));
							DbExt.Query(TShock.DB, "UPDATE 渔获统计 SET 黑檀锦鲤 = 黑檀锦鲤 + @0 WHERE 用户名 = @1", new object[] { 背包第一格数量2, 用户名3 });
							DbExt.Query(TShock.DB, "UPDATE 渔获统计 SET 总量 = 总量 + @0 WHERE 用户名 = @1", new object[] { 背包第一格数量2, 用户名3 });
							DbExt.Query(TShock.DB, "UPDATE 渔获统计 SET 竞赛分 = 竞赛分 + @0 WHERE 用户名 = @1", new object[]
							{
								背包第一格数量2 * 2,
								用户名3
							});
							foreach (渔获奖励 i48 in config2.渔获奖励)
							{
								bool flag271 = i48.鱼类ID作为奖励组 == 手持物品ID2;
								if (flag271)
								{
									foreach (Gofishing.Item1 it52 in i48.包含物品)
									{
										tsplayer2.GiveItem(it52.NetID, it52.Stack * 背包第一格数量2, it52.Perfix);
										it52 = null;
									}
									Gofishing.Item1[] array104 = null;
									args.Player.SendSuccessMessage(string.Format("你通过渔获回收，得到了 {1} 个 {0} 奖励", 手持物品ID2, 背包第一格数量2));
									return;
								}
								i48 = null;
							}
							渔获奖励[] array103 = null;
							args.Player.SendErrorMessage("腐竹没有设置奖励：" + 手持物品ID2 + "   如有需要请在配置文件设置!");
							return;
						}
						bool flag272 = "2319".Equals(手持物品ID2);
						if (flag272)
						{
							args.Player.SendSuccessMessage(string.Format("已成功回收鱼类：[i:{0}]，回收数量：{1}", 手持物品ID2, 背包第一格数量2));
							DbExt.Query(TShock.DB, "UPDATE 渔获统计 SET 血腥食人鱼 = 血腥食人鱼 + @0 WHERE 用户名 = @1", new object[] { 背包第一格数量2, 用户名3 });
							DbExt.Query(TShock.DB, "UPDATE 渔获统计 SET 总量 = 总量 + @0 WHERE 用户名 = @1", new object[] { 背包第一格数量2, 用户名3 });
							DbExt.Query(TShock.DB, "UPDATE 渔获统计 SET 竞赛分 = 竞赛分 + @0 WHERE 用户名 = @1", new object[]
							{
								背包第一格数量2 * 2,
								用户名3
							});
							foreach (渔获奖励 i49 in config2.渔获奖励)
							{
								bool flag273 = i49.鱼类ID作为奖励组 == 手持物品ID2;
								if (flag273)
								{
									foreach (Gofishing.Item1 it53 in i49.包含物品)
									{
										tsplayer2.GiveItem(it53.NetID, it53.Stack * 背包第一格数量2, it53.Perfix);
										it53 = null;
									}
									Gofishing.Item1[] array106 = null;
									args.Player.SendSuccessMessage(string.Format("你通过渔获回收，得到了 {1} 个 {0} 奖励", 手持物品ID2, 背包第一格数量2));
									return;
								}
								i49 = null;
							}
							渔获奖励[] array105 = null;
							args.Player.SendErrorMessage("腐竹没有设置奖励：" + 手持物品ID2 + "   如有需要请在配置文件设置!");
							return;
						}
						bool flag274 = "2321".Equals(手持物品ID2);
						if (flag274)
						{
							args.Player.SendSuccessMessage(string.Format("已成功回收鱼类：[i:{0}]，回收数量：{1}", 手持物品ID2, 背包第一格数量2));
							DbExt.Query(TShock.DB, "UPDATE 渔获统计 SET 臭味鱼 = 臭味鱼 + @0 WHERE 用户名 = @1", new object[] { 背包第一格数量2, 用户名3 });
							DbExt.Query(TShock.DB, "UPDATE 渔获统计 SET 总量 = 总量 + @0 WHERE 用户名 = @1", new object[] { 背包第一格数量2, 用户名3 });
							DbExt.Query(TShock.DB, "UPDATE 渔获统计 SET 竞赛分 = 竞赛分 + @0 WHERE 用户名 = @1", new object[]
							{
								背包第一格数量2 * 2,
								用户名3
							});
							foreach (渔获奖励 i50 in config2.渔获奖励)
							{
								bool flag275 = i50.鱼类ID作为奖励组 == 手持物品ID2;
								if (flag275)
								{
									foreach (Gofishing.Item1 it54 in i50.包含物品)
									{
										tsplayer2.GiveItem(it54.NetID, it54.Stack * 背包第一格数量2, it54.Perfix);
										it54 = null;
									}
									Gofishing.Item1[] array108 = null;
									args.Player.SendSuccessMessage(string.Format("你通过渔获回收，得到了 {1} 个 {0} 奖励", 手持物品ID2, 背包第一格数量2));
									return;
								}
								i50 = null;
							}
							渔获奖励[] array107 = null;
							args.Player.SendErrorMessage("腐竹没有设置奖励：" + 手持物品ID2 + "   如有需要请在配置文件设置!");
							return;
						}
						bool flag276 = "2436".Equals(手持物品ID2);
						if (flag276)
						{
							args.Player.SendSuccessMessage(string.Format("已成功回收鱼类：[i:{0}]，回收数量：{1}", 手持物品ID2, 背包第一格数量2));
							DbExt.Query(TShock.DB, "UPDATE 渔获统计 SET 蓝水母 = 蓝水母 + @0 WHERE 用户名 = @1", new object[] { 背包第一格数量2, 用户名3 });
							DbExt.Query(TShock.DB, "UPDATE 渔获统计 SET 总量 = 总量 + @0 WHERE 用户名 = @1", new object[] { 背包第一格数量2, 用户名3 });
							DbExt.Query(TShock.DB, "UPDATE 渔获统计 SET 竞赛分 = 竞赛分 + @0 WHERE 用户名 = @1", new object[]
							{
								背包第一格数量2 * 2,
								用户名3
							});
							foreach (渔获奖励 i51 in config2.渔获奖励)
							{
								bool flag277 = i51.鱼类ID作为奖励组 == 手持物品ID2;
								if (flag277)
								{
									foreach (Gofishing.Item1 it55 in i51.包含物品)
									{
										tsplayer2.GiveItem(it55.NetID, it55.Stack * 背包第一格数量2, it55.Perfix);
										it55 = null;
									}
									Gofishing.Item1[] array110 = null;
									args.Player.SendSuccessMessage(string.Format("你通过渔获回收，得到了 {1} 个 {0} 奖励", 手持物品ID2, 背包第一格数量2));
									return;
								}
								i51 = null;
							}
							渔获奖励[] array109 = null;
							args.Player.SendErrorMessage("腐竹没有设置奖励：" + 手持物品ID2 + "   如有需要请在配置文件设置!");
							return;
						}
						bool flag278 = "2437".Equals(手持物品ID2);
						if (flag278)
						{
							args.Player.SendSuccessMessage(string.Format("已成功回收鱼类：[i:{0}]，回收数量：{1}", 手持物品ID2, 背包第一格数量2));
							DbExt.Query(TShock.DB, "UPDATE 渔获统计 SET 绿水母 = 绿水母 + @0 WHERE 用户名 = @1", new object[] { 背包第一格数量2, 用户名3 });
							DbExt.Query(TShock.DB, "UPDATE 渔获统计 SET 总量 = 总量 + @0 WHERE 用户名 = @1", new object[] { 背包第一格数量2, 用户名3 });
							DbExt.Query(TShock.DB, "UPDATE 渔获统计 SET 竞赛分 = 竞赛分 + @0 WHERE 用户名 = @1", new object[]
							{
								背包第一格数量2 * 2,
								用户名3
							});
							foreach (渔获奖励 i52 in config2.渔获奖励)
							{
								bool flag279 = i52.鱼类ID作为奖励组 == 手持物品ID2;
								if (flag279)
								{
									foreach (Gofishing.Item1 it56 in i52.包含物品)
									{
										tsplayer2.GiveItem(it56.NetID, it56.Stack * 背包第一格数量2, it56.Perfix);
										it56 = null;
									}
									Gofishing.Item1[] array112 = null;
									args.Player.SendSuccessMessage(string.Format("你通过渔获回收，得到了 {1} 个 {0} 奖励", 手持物品ID2, 背包第一格数量2));
									return;
								}
								i52 = null;
							}
							渔获奖励[] array111 = null;
							args.Player.SendErrorMessage("腐竹没有设置奖励：" + 手持物品ID2 + "   如有需要请在配置文件设置!");
							return;
						}
						bool flag280 = "2438".Equals(手持物品ID2);
						if (flag280)
						{
							args.Player.SendSuccessMessage(string.Format("已成功回收鱼类：[i:{0}]，回收数量：{1}", 手持物品ID2, 背包第一格数量2));
							DbExt.Query(TShock.DB, "UPDATE 渔获统计 SET 粉水母 = 粉水母 + @0 WHERE 用户名 = @1", new object[] { 背包第一格数量2, 用户名3 });
							DbExt.Query(TShock.DB, "UPDATE 渔获统计 SET 总量 = 总量 + @0 WHERE 用户名 = @1", new object[] { 背包第一格数量2, 用户名3 });
							DbExt.Query(TShock.DB, "UPDATE 渔获统计 SET 竞赛分 = 竞赛分 + @0 WHERE 用户名 = @1", new object[]
							{
								背包第一格数量2 * 2,
								用户名3
							});
							foreach (渔获奖励 i53 in config2.渔获奖励)
							{
								bool flag281 = i53.鱼类ID作为奖励组 == 手持物品ID2;
								if (flag281)
								{
									foreach (Gofishing.Item1 it57 in i53.包含物品)
									{
										tsplayer2.GiveItem(it57.NetID, it57.Stack * 背包第一格数量2, it57.Perfix);
										it57 = null;
									}
									Gofishing.Item1[] array114 = null;
									args.Player.SendSuccessMessage(string.Format("你通过渔获回收，得到了 {1} 个 {0} 奖励", 手持物品ID2, 背包第一格数量2));
									return;
								}
								i53 = null;
							}
							渔获奖励[] array113 = null;
							args.Player.SendErrorMessage("腐竹没有设置奖励：" + 手持物品ID2 + "   如有需要请在配置文件设置!");
							return;
						}
						bool flag282 = "4401".Equals(手持物品ID2);
						if (flag282)
						{
							args.Player.SendSuccessMessage(string.Format("已成功回收鱼类：[i:{0}]，回收数量：{1}", 手持物品ID2, 背包第一格数量2));
							DbExt.Query(TShock.DB, "UPDATE 渔获统计 SET 偏口鱼 = 偏口鱼 + @0 WHERE 用户名 = @1", new object[] { 背包第一格数量2, 用户名3 });
							DbExt.Query(TShock.DB, "UPDATE 渔获统计 SET 总量 = 总量 + @0 WHERE 用户名 = @1", new object[] { 背包第一格数量2, 用户名3 });
							DbExt.Query(TShock.DB, "UPDATE 渔获统计 SET 竞赛分 = 竞赛分 + @0 WHERE 用户名 = @1", new object[] { 背包第一格数量2, 用户名3 });
							foreach (渔获奖励 i54 in config2.渔获奖励)
							{
								bool flag283 = i54.鱼类ID作为奖励组 == 手持物品ID2;
								if (flag283)
								{
									foreach (Gofishing.Item1 it58 in i54.包含物品)
									{
										tsplayer2.GiveItem(it58.NetID, it58.Stack * 背包第一格数量2, it58.Perfix);
										it58 = null;
									}
									Gofishing.Item1[] array116 = null;
									args.Player.SendSuccessMessage(string.Format("你通过渔获回收，得到了 {1} 个 {0} 奖励", 手持物品ID2, 背包第一格数量2));
									return;
								}
								i54 = null;
							}
							渔获奖励[] array115 = null;
							args.Player.SendErrorMessage("腐竹没有设置奖励：" + 手持物品ID2 + "   如有需要请在配置文件设置!");
							return;
						}
						bool flag284 = "4402".Equals(手持物品ID2);
						if (flag284)
						{
							args.Player.SendSuccessMessage(string.Format("已成功回收鱼类：[i:{0}]，回收数量：{1}", 手持物品ID2, 背包第一格数量2));
							DbExt.Query(TShock.DB, "UPDATE 渔获统计 SET 岩龙虾 = 岩龙虾 + @0 WHERE 用户名 = @1", new object[] { 背包第一格数量2, 用户名3 });
							DbExt.Query(TShock.DB, "UPDATE 渔获统计 SET 总量 = 总量 + @0 WHERE 用户名 = @1", new object[] { 背包第一格数量2, 用户名3 });
							DbExt.Query(TShock.DB, "UPDATE 渔获统计 SET 竞赛分 = 竞赛分 + @0 WHERE 用户名 = @1", new object[] { 背包第一格数量2, 用户名3 });
							foreach (渔获奖励 i55 in config2.渔获奖励)
							{
								bool flag285 = i55.鱼类ID作为奖励组 == 手持物品ID2;
								if (flag285)
								{
									foreach (Gofishing.Item1 it59 in i55.包含物品)
									{
										tsplayer2.GiveItem(it59.NetID, it59.Stack * 背包第一格数量2, it59.Perfix);
										it59 = null;
									}
									Gofishing.Item1[] array118 = null;
									args.Player.SendSuccessMessage(string.Format("你通过渔获回收，得到了 {1} 个 {0} 奖励", 手持物品ID2, 背包第一格数量2));
									return;
								}
								i55 = null;
							}
							渔获奖励[] array117 = null;
							args.Player.SendErrorMessage("腐竹没有设置奖励：" + 手持物品ID2 + "   如有需要请在配置文件设置!");
							return;
						}
					}
					num35 = k2;
				}
				args.Player.SendErrorMessage("显示栏第一格没放普通鱼，不能回收！");
				return;
				IL_C0A0:
				try
				{
					using (QueryResult _表5 = DbExt.QueryReader(this._数据库, "SELECT * FROM 渔获统计", Array.Empty<object>()))
					{
						while (_表5.Read())
						{
							string _用户名4 = _表5.Get<string>("用户名");
							int _鲈鱼 = _表5.Get<int>("鲈鱼");
							int _鳟鱼 = _表5.Get<int>("鳟鱼");
							int _三文鱼 = _表5.Get<int>("三文鱼");
							int _大西洋鳕鱼 = _表5.Get<int>("大西洋鳕鱼");
							int _金枪鱼 = _表5.Get<int>("金枪鱼");
							int _红鲷鱼 = _表5.Get<int>("红鲷鱼");
							int _霓虹脂鲤 = _表5.Get<int>("霓虹脂鲤");
							int _装甲洞穴鱼 = _表5.Get<int>("装甲洞穴鱼");
							int _雀鲷 = _表5.Get<int>("雀鲷");
							int _猩红虎鱼 = _表5.Get<int>("猩红虎鱼");
							int _寒霜鲦鱼 = _表5.Get<int>("寒霜鲦鱼");
							int _公主鱼 = _表5.Get<int>("公主鱼");
							int _金鲤鱼 = _表5.Get<int>("金鲤鱼");
							int _镜面鱼 = _表5.Get<int>("镜面鱼");
							int _七彩矿鱼 = _表5.Get<int>("七彩矿鱼");
							int _斑驳油鱼 = _表5.Get<int>("斑驳油鱼");
							int _闪鳍锦鲤 = _表5.Get<int>("闪鳍锦鲤");
							int _双鳍鳕鱼 = _表5.Get<int>("双鳍鳕鱼");
							int _黑曜石鱼 = _表5.Get<int>("黑曜石鱼");
							int _虾 = _表5.Get<int>("虾");
							int _混沌鱼 = _表5.Get<int>("混沌鱼");
							int _黑檀锦鲤 = _表5.Get<int>("黑檀锦鲤");
							int _血腥食人鱼 = _表5.Get<int>("血腥食人鱼");
							int _臭味鱼 = _表5.Get<int>("臭味鱼");
							int _蓝水母 = _表5.Get<int>("蓝水母");
							int _绿水母 = _表5.Get<int>("绿水母");
							int _粉水母 = _表5.Get<int>("粉水母");
							int _偏口鱼 = _表5.Get<int>("偏口鱼");
							int _岩龙虾 = _表5.Get<int>("岩龙虾");
							int _总量 = _表5.Get<int>("总量");
							int _总重量 = _表5.Get<int>("总重量");
							bool flag286 = args.Player.Name == _用户名4;
							if (flag286)
							{
								args.Player.SendInfoMessage(args.Player.Name + string.Format("您的渔获：剩余库存 [c/FFFFFF:{0}] 尾，累计捕获重量：[c/FFFFFF:{1} KG]  ----已经捕获到的鱼记录：", _总量, _总重量));
								args.Player.SendInfoMessage("[i:2298]： {0} 尾；  [i:2300]： {1} 尾；  [i:2301]： {2} 尾；", new object[] { _三文鱼, _金枪鱼, _红鲷鱼 });
								args.Player.SendInfoMessage("[i:2307]： {0} 尾；  [i:2308]： {1} 尾；  [i:2309]： {2} 尾；", new object[] { _公主鱼, _金鲤鱼, _镜面鱼 });
								args.Player.SendInfoMessage("[i:2436]： {0} 尾；  [i:2437]： {1} 尾；  [i:2438]： {2} 尾；", new object[] { _蓝水母, _绿水母, _粉水母 });
								args.Player.SendInfoMessage("[i:2290]： {0} 尾；  [i:2297]： {1} 尾；  [i:2304]： {2} 尾；  [i:2316]： {3} 尾；", new object[] { _鲈鱼, _鳟鱼, _雀鲷, _虾 });
								args.Player.SendInfoMessage("[i:2317]： {0} 尾；  [i:2321]： {1} 尾；  [i:4401]： {2} 尾；  [i:4402]： {3} 尾；", new object[] { _混沌鱼, _臭味鱼, _偏口鱼, _岩龙虾 });
								args.Player.SendInfoMessage("[i:2302]： {0} 尾；  [i:2305]： {1} 尾；  [i:2306]： {2} 尾；  [i:2310]： {3} 尾；", new object[] { _霓虹脂鲤, _猩红虎鱼, _寒霜鲦鱼, _七彩矿鱼 });
								args.Player.SendInfoMessage("[i:2312]： {0} 尾；  [i:2313]： {1} 尾；  [i:2315]： {2} 尾；  [i:2318]： {3} 尾；", new object[] { _闪鳍锦鲤, _双鳍鳕鱼, _黑曜石鱼, _黑檀锦鲤 });
								args.Player.SendInfoMessage("[i:2299]： {0} 尾；  [i:2303]： {1} 尾；  [i:2319]： {2} 尾；  [i:2311]： {3} 尾；", new object[] { _大西洋鳕鱼, _装甲洞穴鱼, _血腥食人鱼, _斑驳油鱼 });
								return;
							}
							_用户名4 = null;
						}
					}
					QueryResult _表5 = null;
				}
				catch (Exception ex39)
				{
					Exception ex4 = ex39;
					TShock.Log.ConsoleError(ex4.ToString());
				}
				args.Player.SendSuccessMessage("[i:2487] 您还没有渔获记录 [i:2487]");
				goto IL_12A2E;
				IL_CCD4:
				using (QueryResult _表6 = DbExt.QueryReader(this._数据库, "SELECT * FROM 渔获统计 WHERE 用户名=@0", new object[] { args.Player.Name }))
				{
					if (!_表6.Read())
					{
						args.Player.SendErrorMessage("[i:2487] 您还没有渔获记录，无法兑换货币 [i:2487]");
						return;
					}
				}
				QueryResult _表6 = null;
				try
				{
					int 货币倍数 = Config.GetConfig().货币倍数;
					string SE货币兑换指令 = "";
					string 棱彩货币兑换指令 = "";
					string POBC经验兑换指令 = "";
					int 取兑换数量 = int.Parse(args.Parameters[1]);
					string _用户名5 = "";
					int _总量2 = 0;
					using (QueryResult _表7 = DbExt.QueryReader(this._数据库, "SELECT * FROM 渔获统计 WHERE 用户名=@0", new object[] { args.Player.Name }))
					{
						while (_表7.Read())
						{
							_用户名5 = _表7.Get<string>("用户名");
							_总量2 = _表7.Get<int>("总量");
						}
					}
					QueryResult _表7 = null;
					SE货币兑换指令 = string.Format("/bank give {0} {1} ", _用户名5, 取兑换数量 * 货币倍数);
					棱彩货币兑换指令 = string.Format("/ecom give {0} {1} ", _用户名5, 取兑换数量 * 货币倍数);
					string POBC货币兑换指令 = string.Format("/给钱 {0} {1} ", _用户名5, 取兑换数量 * 货币倍数);
					POBC经验兑换指令 = string.Format("/magclan addexp {0} {1} ", _用户名5, 取兑换数量 * 货币倍数);
					bool flag287 = 取兑换数量 > _总量2;
					if (flag287)
					{
						args.Player.SendErrorMessage(string.Format("[i:2487] 您的渔获库存总数不足[c/FFFFFF:{0}]尾，目前总数是：[c/FFFFFF:{1}] 尾 [i:2487]", 取兑换数量, _总量2));
						return;
					}
					bool flag288 = _总量2 <= 0;
					if (flag288)
					{
						args.Player.SendErrorMessage("你库存的鱼已经兑换完了！请继续回收鱼类！");
						goto IL_12A2E;
					}
					DbExt.Query(TShock.DB, "UPDATE 渔获统计 SET 总量 = 总量 - @0 WHERE 用户名 = @1", new object[] { 取兑换数量, _用户名5 });
					Commands.HandleCommand(TSPlayer.Server, SE货币兑换指令);
					Commands.HandleCommand(TSPlayer.Server, 棱彩货币兑换指令);
					Commands.HandleCommand(TSPlayer.Server, POBC经验兑换指令);
					DbExt.Query(TShock.DB, "UPDATE POBC SET Currency = Currency + @0 WHERE UserName = @1", new object[]
					{
						取兑换数量 * 货币倍数,
						_用户名5
					});
					int 获得货币 = 取兑换数量 * 货币倍数;
					args.Player.SendSuccessMessage(string.Format("你已成功使用 [c/FFFFFF:{0}] 尾鱼，兑换了 [c/FFFFFF:{1}] 货币/exp,目前货币倍数为[c/FFFFFF:{2}]倍！", 取兑换数量, 获得货币, 货币倍数));
					return;
				}
				catch (Exception ex39)
				{
				}
				args.Player.SendErrorMessage("[i:2487] 指令错误，正确指令：/渔获 兑换货币 [提交数量] [i:2487]");
				goto IL_12A2E;
				IL_D18A:
				try
				{
					bool flag289 = args.Parameters[1] == "总数";
					if (flag289)
					{
						try
						{
							string text2 = "[i:2487][c/FF8C00:======↓渔获总数排行↓=========][i:2487]\n";
							int num2 = 1;
							using (QueryResult queryResult3 = DbExt.QueryReader(this._数据库, "SELECT * FROM 渔获统计 ORDER BY 总量 ASC", Array.Empty<object>()))
							{
								while (queryResult3.Read())
								{
									num35 = num2;
									num2 = num35 + 1;
								}
							}
							QueryResult queryResult3 = null;
							bool flag2 = false;
							using (QueryResult queryResult4 = DbExt.QueryReader(this._数据库, "SELECT * FROM 渔获统计 ORDER BY 总量 ASC", Array.Empty<object>()))
							{
								while (queryResult4.Read())
								{
									num35 = num2;
									num2 = num35 - 1;
									flag2 = true;
									string 用户名4 = queryResult4.Get<string>("用户名");
									int 总量3 = queryResult4.Get<int>("总量");
									text2 += string.Format("[c/FFD700:{0}][c/8A2BE2:{1}]→渔获总数：[c/00BFFF:{2}]  尾\n", "【第" + num2.ToString() + "名】", 用户名4, 总量3);
									用户名4 = null;
								}
								bool flag3 = flag2;
								bool flag290 = flag3;
								if (flag290)
								{
									text2 += "[i:2487][c/FF8C00:======↑渔获总数排行↑=========][i:2487]";
									args.Player.SendSuccessMessage(text2);
									return;
								}
							}
							QueryResult queryResult4 = null;
							text2 = null;
						}
						catch (Exception ex39)
						{
							Exception ex5 = ex39;
							TShock.Log.ConsoleError(ex5.ToString());
						}
						args.Player.SendSuccessMessage("[i:2487] 当前服务器没有玩家总数渔获记录 [i:2487]");
						goto IL_12A2E;
					}
					bool flag291 = args.Parameters[1] == "重量";
					if (flag291)
					{
						try
						{
							string text3 = "[i:2487][c/FF8C00:======↓渔获重量排行↓=========][i:2487]\n";
							int num3 = 1;
							using (QueryResult queryResult5 = DbExt.QueryReader(this._数据库, "SELECT * FROM 渔获统计 ORDER BY 总重量 ASC", Array.Empty<object>()))
							{
								while (queryResult5.Read())
								{
									num35 = num3;
									num3 = num35 + 1;
								}
							}
							QueryResult queryResult5 = null;
							bool flag4 = false;
							using (QueryResult queryResult6 = DbExt.QueryReader(this._数据库, "SELECT * FROM 渔获统计 ORDER BY 总重量 ASC", Array.Empty<object>()))
							{
								while (queryResult6.Read())
								{
									num35 = num3;
									num3 = num35 - 1;
									flag4 = true;
									string 用户名5 = queryResult6.Get<string>("用户名");
									int 总重量3 = queryResult6.Get<int>("总重量");
									text3 += string.Format("[c/FFD700:{0}][c/8A2BE2:{1}]→渔获总重量：[c/00BFFF:{2}]  KG\n", "【第" + num3.ToString() + "名】", 用户名5, 总重量3);
									用户名5 = null;
								}
								bool flag5 = flag4;
								bool flag292 = flag5;
								if (flag292)
								{
									text3 += "[i:2487][c/FF8C00:======↑渔获重量排行↑=========][i:2487]";
									args.Player.SendSuccessMessage(text3);
									return;
								}
							}
							QueryResult queryResult6 = null;
							text3 = null;
						}
						catch (Exception ex39)
						{
							Exception ex6 = ex39;
							TShock.Log.ConsoleError(ex6.ToString());
						}
						args.Player.SendSuccessMessage("[i:2487] 当前服务器没有玩家重量渔获记录 [i:2487]");
						goto IL_12A2E;
					}
					bool flag293 = args.Parameters[1] == "大小";
					if (flag293)
					{
						try
						{
							string text4 = "[i:2487][c/FF8C00:======↓渔获最大鱼排行↓=========][i:2487]\n";
							int num4 = 1;
							using (QueryResult queryResult7 = DbExt.QueryReader(this._数据库, "SELECT * FROM 渔获最大 ORDER BY 最大重量 ASC", Array.Empty<object>()))
							{
								while (queryResult7.Read())
								{
									num35 = num4;
									num4 = num35 + 1;
								}
							}
							QueryResult queryResult7 = null;
							bool flag6 = false;
							using (QueryResult queryResult8 = DbExt.QueryReader(this._数据库, "SELECT * FROM 渔获最大 ORDER BY 最大重量 ASC", Array.Empty<object>()))
							{
								while (queryResult8.Read())
								{
									num35 = num4;
									num4 = num35 - 1;
									flag6 = true;
									string 用户名6 = queryResult8.Get<string>("用户名");
									int 鱼ID = queryResult8.Get<int>("鱼ID");
									int 最大重量2 = queryResult8.Get<int>("最大重量");
									text4 += string.Format("[c/FFD700:{0}][c/8A2BE2:{1}]→捕获鱼：[i:{3}]  [c/00BFFF:{2}] KG\n", new object[]
									{
										"【第" + num4.ToString() + "名】",
										用户名6,
										最大重量2,
										鱼ID
									});
									用户名6 = null;
								}
								bool flag7 = flag6;
								bool flag294 = flag7;
								if (flag294)
								{
									text4 += "[i:2487][c/FF8C00:======↑渔获最大鱼排行↑=========][i:2487]";
									args.Player.SendSuccessMessage(text4);
									return;
								}
							}
							QueryResult queryResult8 = null;
							text4 = null;
						}
						catch (Exception ex39)
						{
							Exception ex7 = ex39;
							TShock.Log.ConsoleError(ex7.ToString());
						}
						args.Player.SendSuccessMessage("[i:2487] 当前服务器没有玩家重量渔获记录 [i:2487]");
						goto IL_12A2E;
					}
					bool flag295 = args.Parameters[1] == "鲈鱼" || args.Parameters[1] == "1";
					if (flag295)
					{
						try
						{
							string text5 = "[i:2487][c/FF8C00:======↓鲈鱼重量排行↓=========][i:2487]\n";
							int num5 = 1;
							using (QueryResult queryResult9 = DbExt.QueryReader(this._数据库, "SELECT * FROM 渔获最大 WHERE 鱼ID = 2290 ORDER BY 最大重量 ASC", Array.Empty<object>()))
							{
								while (queryResult9.Read())
								{
									num35 = num5;
									num5 = num35 + 1;
								}
							}
							QueryResult queryResult9 = null;
							bool flag8 = false;
							using (QueryResult queryResult10 = DbExt.QueryReader(this._数据库, "SELECT * FROM 渔获最大 WHERE 鱼ID = 2290 ORDER BY 最大重量 ASC", Array.Empty<object>()))
							{
								while (queryResult10.Read())
								{
									num35 = num5;
									num5 = num35 - 1;
									flag8 = true;
									string 用户名7 = queryResult10.Get<string>("用户名");
									int 最大重量3 = queryResult10.Get<int>("最大重量");
									text5 += string.Format("[c/FFD700:{0}][c/8A2BE2:{1}]→鲈鱼重量：[c/00BFFF:{2}]  KG\n", "【第" + num5.ToString() + "名】", 用户名7, 最大重量3);
									用户名7 = null;
								}
								bool flag9 = flag8;
								bool flag296 = flag9;
								if (flag296)
								{
									text5 += "[i:2487][c/FF8C00:======↑鲈鱼重量排行↑=========][i:2487]";
									args.Player.SendSuccessMessage(text5);
									return;
								}
							}
							QueryResult queryResult10 = null;
							text5 = null;
						}
						catch (Exception ex39)
						{
							Exception ex8 = ex39;
							TShock.Log.ConsoleError(ex8.ToString());
						}
						args.Player.SendSuccessMessage("[i:2487] 还没有鲈鱼排行数据！ [i:2487]");
						goto IL_12A2E;
					}
					bool flag297 = args.Parameters[1] == "鳟鱼" || args.Parameters[1] == "2";
					if (flag297)
					{
						try
						{
							string text6 = "[i:2487][c/FF8C00:======↓鳟鱼重量排行↓=========][i:2487]\n";
							int num6 = 1;
							using (QueryResult queryResult11 = DbExt.QueryReader(this._数据库, "SELECT * FROM 渔获最大 WHERE 鱼ID = 2297 ORDER BY 最大重量 ASC", Array.Empty<object>()))
							{
								while (queryResult11.Read())
								{
									num35 = num6;
									num6 = num35 + 1;
								}
							}
							QueryResult queryResult11 = null;
							bool flag10 = false;
							using (QueryResult queryResult12 = DbExt.QueryReader(this._数据库, "SELECT * FROM 渔获最大 WHERE 鱼ID = 2297 ORDER BY 最大重量 ASC", Array.Empty<object>()))
							{
								while (queryResult12.Read())
								{
									num35 = num6;
									num6 = num35 - 1;
									flag10 = true;
									string 用户名8 = queryResult12.Get<string>("用户名");
									int 最大重量4 = queryResult12.Get<int>("最大重量");
									text6 += string.Format("[c/FFD700:{0}][c/8A2BE2:{1}]→鳟鱼重量：[c/00BFFF:{2}]  KG\n", "【第" + num6.ToString() + "名】", 用户名8, 最大重量4);
									用户名8 = null;
								}
								bool flag11 = flag10;
								bool flag298 = flag11;
								if (flag298)
								{
									text6 += "[i:2487][c/FF8C00:======↑鳟鱼重量排行↑=========][i:2487]";
									args.Player.SendSuccessMessage(text6);
									return;
								}
							}
							QueryResult queryResult12 = null;
							text6 = null;
						}
						catch (Exception ex39)
						{
							Exception ex9 = ex39;
							TShock.Log.ConsoleError(ex9.ToString());
						}
						args.Player.SendSuccessMessage("[i:2487] 还没有鳟鱼排行数据！ [i:2487]");
						goto IL_12A2E;
					}
					bool flag299 = args.Parameters[1] == "三文鱼" || args.Parameters[1] == "3";
					if (flag299)
					{
						try
						{
							string text7 = "[i:2487][c/FF8C00:======↓三文鱼重量排行↓=========][i:2487]\n";
							int num7 = 1;
							using (QueryResult queryResult13 = DbExt.QueryReader(this._数据库, "SELECT * FROM 渔获最大 WHERE 鱼ID = 2298 ORDER BY 最大重量 ASC", Array.Empty<object>()))
							{
								while (queryResult13.Read())
								{
									num35 = num7;
									num7 = num35 + 1;
								}
							}
							QueryResult queryResult13 = null;
							bool flag12 = false;
							using (QueryResult queryResult14 = DbExt.QueryReader(this._数据库, "SELECT * FROM 渔获最大 WHERE 鱼ID = 2298 ORDER BY 最大重量 ASC", Array.Empty<object>()))
							{
								while (queryResult14.Read())
								{
									num35 = num7;
									num7 = num35 - 1;
									flag12 = true;
									string 用户名9 = queryResult14.Get<string>("用户名");
									int 最大重量5 = queryResult14.Get<int>("最大重量");
									text7 += string.Format("[c/FFD700:{0}][c/8A2BE2:{1}]→三文鱼重量：[c/00BFFF:{2}]  KG\n", "【第" + num7.ToString() + "名】", 用户名9, 最大重量5);
									用户名9 = null;
								}
								bool flag13 = flag12;
								bool flag300 = flag13;
								if (flag300)
								{
									text7 += "[i:2487][c/FF8C00:======↑三文鱼重量排行↑=========][i:2487]";
									args.Player.SendSuccessMessage(text7);
									return;
								}
							}
							QueryResult queryResult14 = null;
							text7 = null;
						}
						catch (Exception ex39)
						{
							Exception ex10 = ex39;
							TShock.Log.ConsoleError(ex10.ToString());
						}
						args.Player.SendSuccessMessage("[i:2487] 还没有三文鱼排行数据！ [i:2487]");
						goto IL_12A2E;
					}
					bool flag301 = args.Parameters[1] == "大西洋鳕鱼" || args.Parameters[1] == "4";
					if (flag301)
					{
						try
						{
							string text8 = "[i:2487][c/FF8C00:======↓大西洋鳕鱼重量排行↓=========][i:2487]\n";
							int num8 = 1;
							using (QueryResult queryResult15 = DbExt.QueryReader(this._数据库, "SELECT * FROM 渔获最大 WHERE 鱼ID = 2299 ORDER BY 最大重量 ASC", Array.Empty<object>()))
							{
								while (queryResult15.Read())
								{
									num35 = num8;
									num8 = num35 + 1;
								}
							}
							QueryResult queryResult15 = null;
							bool flag14 = false;
							using (QueryResult queryResult16 = DbExt.QueryReader(this._数据库, "SELECT * FROM 渔获最大 WHERE 鱼ID = 2299 ORDER BY 最大重量 ASC", Array.Empty<object>()))
							{
								while (queryResult16.Read())
								{
									num35 = num8;
									num8 = num35 - 1;
									flag14 = true;
									string 用户名10 = queryResult16.Get<string>("用户名");
									int 最大重量6 = queryResult16.Get<int>("最大重量");
									text8 += string.Format("[c/FFD700:{0}][c/8A2BE2:{1}]→大西洋鳕鱼重量：[c/00BFFF:{2}]  KG\n", "【第" + num8.ToString() + "名】", 用户名10, 最大重量6);
									用户名10 = null;
								}
								bool flag15 = flag14;
								bool flag302 = flag15;
								if (flag302)
								{
									text8 += "[i:2487][c/FF8C00:======↑大西洋鳕鱼重量排行↑=========][i:2487]";
									args.Player.SendSuccessMessage(text8);
									return;
								}
							}
							QueryResult queryResult16 = null;
							text8 = null;
						}
						catch (Exception ex39)
						{
							Exception ex11 = ex39;
							TShock.Log.ConsoleError(ex11.ToString());
						}
						args.Player.SendSuccessMessage("[i:2487] 还没有大西洋鳕鱼排行数据！ [i:2487]");
						goto IL_12A2E;
					}
					bool flag303 = args.Parameters[1] == "金枪鱼" || args.Parameters[1] == "5";
					if (flag303)
					{
						try
						{
							string text9 = "[i:2487][c/FF8C00:======↓金枪鱼重量排行↓=========][i:2487]\n";
							int num9 = 1;
							using (QueryResult queryResult17 = DbExt.QueryReader(this._数据库, "SELECT * FROM 渔获最大 WHERE 鱼ID = 2300 ORDER BY 最大重量 ASC", Array.Empty<object>()))
							{
								while (queryResult17.Read())
								{
									num35 = num9;
									num9 = num35 + 1;
								}
							}
							QueryResult queryResult17 = null;
							bool flag16 = false;
							using (QueryResult queryResult18 = DbExt.QueryReader(this._数据库, "SELECT * FROM 渔获最大 WHERE 鱼ID = 2300 ORDER BY 最大重量 ASC", Array.Empty<object>()))
							{
								while (queryResult18.Read())
								{
									num35 = num9;
									num9 = num35 - 1;
									flag16 = true;
									string 用户名11 = queryResult18.Get<string>("用户名");
									int 最大重量7 = queryResult18.Get<int>("最大重量");
									text9 += string.Format("[c/FFD700:{0}][c/8A2BE2:{1}]→金枪鱼重量：[c/00BFFF:{2}]  KG\n", "【第" + num9.ToString() + "名】", 用户名11, 最大重量7);
									用户名11 = null;
								}
								bool flag17 = flag16;
								bool flag304 = flag17;
								if (flag304)
								{
									text9 += "[i:2487][c/FF8C00:======↑金枪鱼重量排行↑=========][i:2487]";
									args.Player.SendSuccessMessage(text9);
									return;
								}
							}
							QueryResult queryResult18 = null;
							text9 = null;
						}
						catch (Exception ex39)
						{
							Exception ex12 = ex39;
							TShock.Log.ConsoleError(ex12.ToString());
						}
						args.Player.SendSuccessMessage("[i:2487] 还没有金枪鱼排行数据！ [i:2487]");
						goto IL_12A2E;
					}
					bool flag305 = args.Parameters[1] == "红鲷鱼" || args.Parameters[1] == "6";
					if (flag305)
					{
						try
						{
							string text10 = "[i:2487][c/FF8C00:======↓红鲷鱼重量排行↓=========][i:2487]\n";
							int num10 = 1;
							using (QueryResult queryResult19 = DbExt.QueryReader(this._数据库, "SELECT * FROM 渔获最大 WHERE 鱼ID = 2301 ORDER BY 最大重量 ASC", Array.Empty<object>()))
							{
								while (queryResult19.Read())
								{
									num35 = num10;
									num10 = num35 + 1;
								}
							}
							QueryResult queryResult19 = null;
							bool flag18 = false;
							using (QueryResult queryResult20 = DbExt.QueryReader(this._数据库, "SELECT * FROM 渔获最大 WHERE 鱼ID = 2301 ORDER BY 最大重量 ASC", Array.Empty<object>()))
							{
								while (queryResult20.Read())
								{
									num35 = num10;
									num10 = num35 - 1;
									flag18 = true;
									string 用户名12 = queryResult20.Get<string>("用户名");
									int 最大重量8 = queryResult20.Get<int>("最大重量");
									text10 += string.Format("[c/FFD700:{0}][c/8A2BE2:{1}]→红鲷鱼重量：[c/00BFFF:{2}]  KG\n", "【第" + num10.ToString() + "名】", 用户名12, 最大重量8);
									用户名12 = null;
								}
								bool flag19 = flag18;
								bool flag306 = flag19;
								if (flag306)
								{
									text10 += "[i:2487][c/FF8C00:======↑红鲷鱼重量排行↑=========][i:2487]";
									args.Player.SendSuccessMessage(text10);
									return;
								}
							}
							QueryResult queryResult20 = null;
							text10 = null;
						}
						catch (Exception ex39)
						{
							Exception ex13 = ex39;
							TShock.Log.ConsoleError(ex13.ToString());
						}
						args.Player.SendSuccessMessage("[i:2487] 还没有红鲷鱼排行数据！ [i:2487]");
						goto IL_12A2E;
					}
					bool flag307 = args.Parameters[1] == "霓虹脂鲤" || args.Parameters[1] == "7";
					if (flag307)
					{
						try
						{
							string text11 = "[i:2487][c/FF8C00:======↓霓虹脂鲤重量排行↓=========][i:2487]\n";
							int num11 = 1;
							using (QueryResult queryResult21 = DbExt.QueryReader(this._数据库, "SELECT * FROM 渔获最大 WHERE 鱼ID = 2302 ORDER BY 最大重量 ASC", Array.Empty<object>()))
							{
								while (queryResult21.Read())
								{
									num35 = num11;
									num11 = num35 + 1;
								}
							}
							QueryResult queryResult21 = null;
							bool flag20 = false;
							using (QueryResult queryResult22 = DbExt.QueryReader(this._数据库, "SELECT * FROM 渔获最大 WHERE 鱼ID = 2302 ORDER BY 最大重量 ASC", Array.Empty<object>()))
							{
								while (queryResult22.Read())
								{
									num35 = num11;
									num11 = num35 - 1;
									flag20 = true;
									string 用户名13 = queryResult22.Get<string>("用户名");
									int 最大重量9 = queryResult22.Get<int>("最大重量");
									text11 += string.Format("[c/FFD700:{0}][c/8A2BE2:{1}]→霓虹脂鲤重量：[c/00BFFF:{2}]  KG\n", "【第" + num11.ToString() + "名】", 用户名13, 最大重量9);
									用户名13 = null;
								}
								bool flag21 = flag20;
								bool flag308 = flag21;
								if (flag308)
								{
									text11 += "[i:2487][c/FF8C00:======↑霓虹脂鲤重量排行↑=========][i:2487]";
									args.Player.SendSuccessMessage(text11);
									return;
								}
							}
							QueryResult queryResult22 = null;
							text11 = null;
						}
						catch (Exception ex39)
						{
							Exception ex14 = ex39;
							TShock.Log.ConsoleError(ex14.ToString());
						}
						args.Player.SendSuccessMessage("[i:2487] 还没有霓虹脂鲤排行数据！ [i:2487]");
						goto IL_12A2E;
					}
					bool flag309 = args.Parameters[1] == "装甲洞穴鱼" || args.Parameters[1] == "8";
					if (flag309)
					{
						try
						{
							string text12 = "[i:2487][c/FF8C00:======↓装甲洞穴鱼重量排行↓=========][i:2487]\n";
							int num12 = 1;
							using (QueryResult queryResult23 = DbExt.QueryReader(this._数据库, "SELECT * FROM 渔获最大 WHERE 鱼ID = 2303 ORDER BY 最大重量 ASC", Array.Empty<object>()))
							{
								while (queryResult23.Read())
								{
									num35 = num12;
									num12 = num35 + 1;
								}
							}
							QueryResult queryResult23 = null;
							bool flag22 = false;
							using (QueryResult queryResult24 = DbExt.QueryReader(this._数据库, "SELECT * FROM 渔获最大 WHERE 鱼ID = 2303 ORDER BY 最大重量 ASC", Array.Empty<object>()))
							{
								while (queryResult24.Read())
								{
									num35 = num12;
									num12 = num35 - 1;
									flag22 = true;
									string 用户名14 = queryResult24.Get<string>("用户名");
									int 最大重量10 = queryResult24.Get<int>("最大重量");
									text12 += string.Format("[c/FFD700:{0}][c/8A2BE2:{1}]→装甲洞穴鱼重量：[c/00BFFF:{2}]  KG\n", "【第" + num12.ToString() + "名】", 用户名14, 最大重量10);
									用户名14 = null;
								}
								bool flag23 = flag22;
								bool flag310 = flag23;
								if (flag310)
								{
									text12 += "[i:2487][c/FF8C00:======↑装甲洞穴鱼重量排行↑=========][i:2487]";
									args.Player.SendSuccessMessage(text12);
									return;
								}
							}
							QueryResult queryResult24 = null;
							text12 = null;
						}
						catch (Exception ex39)
						{
							Exception ex15 = ex39;
							TShock.Log.ConsoleError(ex15.ToString());
						}
						args.Player.SendSuccessMessage("[i:2487] 还没有装甲洞穴鱼排行数据！ [i:2487]");
						goto IL_12A2E;
					}
					bool flag311 = args.Parameters[1] == "雀鲷" || args.Parameters[1] == "9";
					if (flag311)
					{
						try
						{
							string text13 = "[i:2487][c/FF8C00:======↓雀鲷重量排行↓=========][i:2487]\n";
							int num13 = 1;
							using (QueryResult queryResult25 = DbExt.QueryReader(this._数据库, "SELECT * FROM 渔获最大 WHERE 鱼ID = 2304 ORDER BY 最大重量 ASC", Array.Empty<object>()))
							{
								while (queryResult25.Read())
								{
									num35 = num13;
									num13 = num35 + 1;
								}
							}
							QueryResult queryResult25 = null;
							bool flag24 = false;
							using (QueryResult queryResult26 = DbExt.QueryReader(this._数据库, "SELECT * FROM 渔获最大 WHERE 鱼ID = 2304 ORDER BY 最大重量 ASC", Array.Empty<object>()))
							{
								while (queryResult26.Read())
								{
									num35 = num13;
									num13 = num35 - 1;
									flag24 = true;
									string 用户名15 = queryResult26.Get<string>("用户名");
									int 最大重量11 = queryResult26.Get<int>("最大重量");
									text13 += string.Format("[c/FFD700:{0}][c/8A2BE2:{1}]→雀鲷重量：[c/00BFFF:{2}]  KG\n", "【第" + num13.ToString() + "名】", 用户名15, 最大重量11);
									用户名15 = null;
								}
								bool flag25 = flag24;
								bool flag312 = flag25;
								if (flag312)
								{
									text13 += "[i:2487][c/FF8C00:======↑雀鲷重量排行↑=========][i:2487]";
									args.Player.SendSuccessMessage(text13);
									return;
								}
							}
							QueryResult queryResult26 = null;
							text13 = null;
						}
						catch (Exception ex39)
						{
							Exception ex16 = ex39;
							TShock.Log.ConsoleError(ex16.ToString());
						}
						args.Player.SendSuccessMessage("[i:2487] 还没有雀鲷排行数据！ [i:2487]");
						goto IL_12A2E;
					}
					bool flag313 = args.Parameters[1] == "猩红虎鱼" || args.Parameters[1] == "10";
					if (flag313)
					{
						try
						{
							string text14 = "[i:2487][c/FF8C00:======↓猩红虎鱼重量排行↓=========][i:2487]\n";
							int num14 = 1;
							using (QueryResult queryResult27 = DbExt.QueryReader(this._数据库, "SELECT * FROM 渔获最大 WHERE 鱼ID = 2305 ORDER BY 最大重量 ASC", Array.Empty<object>()))
							{
								while (queryResult27.Read())
								{
									num35 = num14;
									num14 = num35 + 1;
								}
							}
							QueryResult queryResult27 = null;
							bool flag26 = false;
							using (QueryResult queryResult28 = DbExt.QueryReader(this._数据库, "SELECT * FROM 渔获最大 WHERE 鱼ID = 2305 ORDER BY 最大重量 ASC", Array.Empty<object>()))
							{
								while (queryResult28.Read())
								{
									num35 = num14;
									num14 = num35 - 1;
									flag26 = true;
									string 用户名16 = queryResult28.Get<string>("用户名");
									int 最大重量12 = queryResult28.Get<int>("最大重量");
									text14 += string.Format("[c/FFD700:{0}][c/8A2BE2:{1}]→猩红虎鱼重量：[c/00BFFF:{2}]  KG\n", "【第" + num14.ToString() + "名】", 用户名16, 最大重量12);
									用户名16 = null;
								}
								bool flag27 = flag26;
								bool flag314 = flag27;
								if (flag314)
								{
									text14 += "[i:2487][c/FF8C00:======↑猩红虎鱼重量排行↑=========][i:2487]";
									args.Player.SendSuccessMessage(text14);
									return;
								}
							}
							QueryResult queryResult28 = null;
							text14 = null;
						}
						catch (Exception ex39)
						{
							Exception ex17 = ex39;
							TShock.Log.ConsoleError(ex17.ToString());
						}
						args.Player.SendSuccessMessage("[i:2487] 还没有猩红虎鱼排行数据！ [i:2487]");
						goto IL_12A2E;
					}
					bool flag315 = args.Parameters[1] == "寒霜鲦鱼" || args.Parameters[1] == "11";
					if (flag315)
					{
						try
						{
							string text15 = "[i:2487][c/FF8C00:======↓寒霜鲦鱼重量排行↓=========][i:2487]\n";
							int num15 = 1;
							using (QueryResult queryResult29 = DbExt.QueryReader(this._数据库, "SELECT * FROM 渔获最大 WHERE 鱼ID = 2306 ORDER BY 最大重量 ASC", Array.Empty<object>()))
							{
								while (queryResult29.Read())
								{
									num35 = num15;
									num15 = num35 + 1;
								}
							}
							QueryResult queryResult29 = null;
							bool flag28 = false;
							using (QueryResult queryResult30 = DbExt.QueryReader(this._数据库, "SELECT * FROM 渔获最大 WHERE 鱼ID = 2306 ORDER BY 最大重量 ASC", Array.Empty<object>()))
							{
								while (queryResult30.Read())
								{
									num35 = num15;
									num15 = num35 - 1;
									flag28 = true;
									string 用户名17 = queryResult30.Get<string>("用户名");
									int 最大重量13 = queryResult30.Get<int>("最大重量");
									text15 += string.Format("[c/FFD700:{0}][c/8A2BE2:{1}]→寒霜鲦鱼重量：[c/00BFFF:{2}]  KG\n", "【第" + num15.ToString() + "名】", 用户名17, 最大重量13);
									用户名17 = null;
								}
								bool flag29 = flag28;
								bool flag316 = flag29;
								if (flag316)
								{
									text15 += "[i:2487][c/FF8C00:======↑寒霜鲦鱼重量排行↑=========][i:2487]";
									args.Player.SendSuccessMessage(text15);
									return;
								}
							}
							QueryResult queryResult30 = null;
							text15 = null;
						}
						catch (Exception ex39)
						{
							Exception ex18 = ex39;
							TShock.Log.ConsoleError(ex18.ToString());
						}
						args.Player.SendSuccessMessage("[i:2487] 还没有寒霜鲦鱼排行数据！ [i:2487]");
						goto IL_12A2E;
					}
					bool flag317 = args.Parameters[1] == "公主鱼" || args.Parameters[1] == "12";
					if (flag317)
					{
						try
						{
							string text16 = "[i:2487][c/FF8C00:======↓公主鱼重量排行↓=========][i:2487]\n";
							int num16 = 1;
							using (QueryResult queryResult31 = DbExt.QueryReader(this._数据库, "SELECT * FROM 渔获最大 WHERE 鱼ID = 2307 ORDER BY 最大重量 ASC", Array.Empty<object>()))
							{
								while (queryResult31.Read())
								{
									num35 = num16;
									num16 = num35 + 1;
								}
							}
							QueryResult queryResult31 = null;
							bool flag30 = false;
							using (QueryResult queryResult32 = DbExt.QueryReader(this._数据库, "SELECT * FROM 渔获最大 WHERE 鱼ID = 2307 ORDER BY 最大重量 ASC", Array.Empty<object>()))
							{
								while (queryResult32.Read())
								{
									num35 = num16;
									num16 = num35 - 1;
									flag30 = true;
									string 用户名18 = queryResult32.Get<string>("用户名");
									int 最大重量14 = queryResult32.Get<int>("最大重量");
									text16 += string.Format("[c/FFD700:{0}][c/8A2BE2:{1}]→公主鱼重量：[c/00BFFF:{2}]  KG\n", "【第" + num16.ToString() + "名】", 用户名18, 最大重量14);
									用户名18 = null;
								}
								bool flag31 = flag30;
								bool flag318 = flag31;
								if (flag318)
								{
									text16 += "[i:2487][c/FF8C00:======↑公主鱼重量排行↑=========][i:2487]";
									args.Player.SendSuccessMessage(text16);
									return;
								}
							}
							QueryResult queryResult32 = null;
							text16 = null;
						}
						catch (Exception ex39)
						{
							Exception ex19 = ex39;
							TShock.Log.ConsoleError(ex19.ToString());
						}
						args.Player.SendSuccessMessage("[i:2487] 还没有公主鱼排行数据！ [i:2487]");
						goto IL_12A2E;
					}
					bool flag319 = args.Parameters[1] == "金鲤鱼" || args.Parameters[1] == "13";
					if (flag319)
					{
						try
						{
							string text17 = "[i:2487][c/FF8C00:======↓金鲤鱼重量排行↓=========][i:2487]\n";
							int num17 = 1;
							using (QueryResult queryResult33 = DbExt.QueryReader(this._数据库, "SELECT * FROM 渔获最大 WHERE 鱼ID = 2308 ORDER BY 最大重量 ASC", Array.Empty<object>()))
							{
								while (queryResult33.Read())
								{
									num35 = num17;
									num17 = num35 + 1;
								}
							}
							QueryResult queryResult33 = null;
							bool flag32 = false;
							using (QueryResult queryResult34 = DbExt.QueryReader(this._数据库, "SELECT * FROM 渔获最大 WHERE 鱼ID = 2308 ORDER BY 最大重量 ASC", Array.Empty<object>()))
							{
								while (queryResult34.Read())
								{
									num35 = num17;
									num17 = num35 - 1;
									flag32 = true;
									string 用户名19 = queryResult34.Get<string>("用户名");
									int 最大重量15 = queryResult34.Get<int>("最大重量");
									text17 += string.Format("[c/FFD700:{0}][c/8A2BE2:{1}]→金鲤鱼重量：[c/00BFFF:{2}]  KG\n", "【第" + num17.ToString() + "名】", 用户名19, 最大重量15);
									用户名19 = null;
								}
								bool flag33 = flag32;
								bool flag320 = flag33;
								if (flag320)
								{
									text17 += "[i:2487][c/FF8C00:======↑金鲤鱼重量排行↑=========][i:2487]";
									args.Player.SendSuccessMessage(text17);
									return;
								}
							}
							QueryResult queryResult34 = null;
							text17 = null;
						}
						catch (Exception ex39)
						{
							Exception ex20 = ex39;
							TShock.Log.ConsoleError(ex20.ToString());
						}
						args.Player.SendSuccessMessage("[i:2487] 还没有金鲤鱼排行数据！ [i:2487]");
						goto IL_12A2E;
					}
					bool flag321 = args.Parameters[1] == "镜面鱼" || args.Parameters[1] == "14";
					if (flag321)
					{
						try
						{
							string text18 = "[i:2487][c/FF8C00:======↓镜面鱼重量排行↓=========][i:2487]\n";
							int num18 = 1;
							using (QueryResult queryResult35 = DbExt.QueryReader(this._数据库, "SELECT * FROM 渔获最大 WHERE 鱼ID = 2309 ORDER BY 最大重量 ASC", Array.Empty<object>()))
							{
								while (queryResult35.Read())
								{
									num35 = num18;
									num18 = num35 + 1;
								}
							}
							QueryResult queryResult35 = null;
							bool flag34 = false;
							using (QueryResult queryResult36 = DbExt.QueryReader(this._数据库, "SELECT * FROM 渔获最大 WHERE 鱼ID = 2309 ORDER BY 最大重量 ASC", Array.Empty<object>()))
							{
								while (queryResult36.Read())
								{
									num35 = num18;
									num18 = num35 - 1;
									flag34 = true;
									string 用户名20 = queryResult36.Get<string>("用户名");
									int 最大重量16 = queryResult36.Get<int>("最大重量");
									text18 += string.Format("[c/FFD700:{0}][c/8A2BE2:{1}]→镜面鱼重量：[c/00BFFF:{2}]  KG\n", "【第" + num18.ToString() + "名】", 用户名20, 最大重量16);
									用户名20 = null;
								}
								bool flag35 = flag34;
								bool flag322 = flag35;
								if (flag322)
								{
									text18 += "[i:2487][c/FF8C00:======↑镜面鱼重量排行↑=========][i:2487]";
									args.Player.SendSuccessMessage(text18);
									return;
								}
							}
							QueryResult queryResult36 = null;
							text18 = null;
						}
						catch (Exception ex39)
						{
							Exception ex21 = ex39;
							TShock.Log.ConsoleError(ex21.ToString());
						}
						args.Player.SendSuccessMessage("[i:2487] 还没有镜面鱼排行数据！ [i:2487]");
						goto IL_12A2E;
					}
					bool flag323 = args.Parameters[1] == "七彩矿鱼" || args.Parameters[1] == "15";
					if (flag323)
					{
						try
						{
							string text19 = "[i:2487][c/FF8C00:======↓七彩矿鱼重量排行↓=========][i:2487]\n";
							int num19 = 1;
							using (QueryResult queryResult37 = DbExt.QueryReader(this._数据库, "SELECT * FROM 渔获最大 WHERE 鱼ID = 2310 ORDER BY 最大重量 ASC", Array.Empty<object>()))
							{
								while (queryResult37.Read())
								{
									num35 = num19;
									num19 = num35 + 1;
								}
							}
							QueryResult queryResult37 = null;
							bool flag36 = false;
							using (QueryResult queryResult38 = DbExt.QueryReader(this._数据库, "SELECT * FROM 渔获最大 WHERE 鱼ID = 2310 ORDER BY 最大重量 ASC", Array.Empty<object>()))
							{
								while (queryResult38.Read())
								{
									num35 = num19;
									num19 = num35 - 1;
									flag36 = true;
									string 用户名21 = queryResult38.Get<string>("用户名");
									int 最大重量17 = queryResult38.Get<int>("最大重量");
									text19 += string.Format("[c/FFD700:{0}][c/8A2BE2:{1}]→七彩矿鱼重量：[c/00BFFF:{2}]  KG\n", "【第" + num19.ToString() + "名】", 用户名21, 最大重量17);
									用户名21 = null;
								}
								bool flag37 = flag36;
								bool flag324 = flag37;
								if (flag324)
								{
									text19 += "[i:2487][c/FF8C00:======↑七彩矿鱼重量排行↑=========][i:2487]";
									args.Player.SendSuccessMessage(text19);
									return;
								}
							}
							QueryResult queryResult38 = null;
							text19 = null;
						}
						catch (Exception ex39)
						{
							Exception ex22 = ex39;
							TShock.Log.ConsoleError(ex22.ToString());
						}
						args.Player.SendSuccessMessage("[i:2487] 还没有七彩矿鱼排行数据！ [i:2487]");
						goto IL_12A2E;
					}
					bool flag325 = args.Parameters[1] == "斑驳油鱼" || args.Parameters[1] == "16";
					if (flag325)
					{
						try
						{
							string text20 = "[i:2487][c/FF8C00:======↓斑驳油鱼重量排行↓=========][i:2487]\n";
							int num20 = 1;
							using (QueryResult queryResult39 = DbExt.QueryReader(this._数据库, "SELECT * FROM 渔获最大 WHERE 鱼ID = 2311 ORDER BY 最大重量 ASC", Array.Empty<object>()))
							{
								while (queryResult39.Read())
								{
									num35 = num20;
									num20 = num35 + 1;
								}
							}
							QueryResult queryResult39 = null;
							bool flag38 = false;
							using (QueryResult queryResult40 = DbExt.QueryReader(this._数据库, "SELECT * FROM 渔获最大 WHERE 鱼ID = 2311 ORDER BY 最大重量 ASC", Array.Empty<object>()))
							{
								while (queryResult40.Read())
								{
									num35 = num20;
									num20 = num35 - 1;
									flag38 = true;
									string 用户名22 = queryResult40.Get<string>("用户名");
									int 最大重量18 = queryResult40.Get<int>("最大重量");
									text20 += string.Format("[c/FFD700:{0}][c/8A2BE2:{1}]→斑驳油鱼重量：[c/00BFFF:{2}]  KG\n", "【第" + num20.ToString() + "名】", 用户名22, 最大重量18);
									用户名22 = null;
								}
								bool flag39 = flag38;
								bool flag326 = flag39;
								if (flag326)
								{
									text20 += "[i:2487][c/FF8C00:======↑斑驳油鱼重量排行↑=========][i:2487]";
									args.Player.SendSuccessMessage(text20);
									return;
								}
							}
							QueryResult queryResult40 = null;
							text20 = null;
						}
						catch (Exception ex39)
						{
							Exception ex23 = ex39;
							TShock.Log.ConsoleError(ex23.ToString());
						}
						args.Player.SendSuccessMessage("[i:2487] 还没有斑驳油鱼排行数据！ [i:2487]");
						goto IL_12A2E;
					}
					bool flag327 = args.Parameters[1] == "闪鳍锦鲤" || args.Parameters[1] == "17";
					if (flag327)
					{
						try
						{
							string text21 = "[i:2487][c/FF8C00:======↓闪鳍锦鲤重量排行↓=========][i:2487]\n";
							int num21 = 1;
							using (QueryResult queryResult41 = DbExt.QueryReader(this._数据库, "SELECT * FROM 渔获最大 WHERE 鱼ID = 2312 ORDER BY 最大重量 ASC", Array.Empty<object>()))
							{
								while (queryResult41.Read())
								{
									num35 = num21;
									num21 = num35 + 1;
								}
							}
							QueryResult queryResult41 = null;
							bool flag40 = false;
							using (QueryResult queryResult42 = DbExt.QueryReader(this._数据库, "SELECT * FROM 渔获最大 WHERE 鱼ID = 2312 ORDER BY 最大重量 ASC", Array.Empty<object>()))
							{
								while (queryResult42.Read())
								{
									num35 = num21;
									num21 = num35 - 1;
									flag40 = true;
									string 用户名23 = queryResult42.Get<string>("用户名");
									int 最大重量19 = queryResult42.Get<int>("最大重量");
									text21 += string.Format("[c/FFD700:{0}][c/8A2BE2:{1}]→闪鳍锦鲤重量：[c/00BFFF:{2}]  KG\n", "【第" + num21.ToString() + "名】", 用户名23, 最大重量19);
									用户名23 = null;
								}
								bool flag41 = flag40;
								bool flag328 = flag41;
								if (flag328)
								{
									text21 += "[i:2487][c/FF8C00:======↑闪鳍锦鲤重量排行↑=========][i:2487]";
									args.Player.SendSuccessMessage(text21);
									return;
								}
							}
							QueryResult queryResult42 = null;
							text21 = null;
						}
						catch (Exception ex39)
						{
							Exception ex24 = ex39;
							TShock.Log.ConsoleError(ex24.ToString());
						}
						args.Player.SendSuccessMessage("[i:2487] 还没有闪鳍锦鲤排行数据！ [i:2487]");
						goto IL_12A2E;
					}
					bool flag329 = args.Parameters[1] == "双鳍鳕鱼" || args.Parameters[1] == "18";
					if (flag329)
					{
						try
						{
							string text22 = "[i:2487][c/FF8C00:======↓双鳍鳕鱼重量排行↓=========][i:2487]\n";
							int num22 = 1;
							using (QueryResult queryResult43 = DbExt.QueryReader(this._数据库, "SELECT * FROM 渔获最大 WHERE 鱼ID = 2313 ORDER BY 最大重量 ASC", Array.Empty<object>()))
							{
								while (queryResult43.Read())
								{
									num35 = num22;
									num22 = num35 + 1;
								}
							}
							QueryResult queryResult43 = null;
							bool flag42 = false;
							using (QueryResult queryResult44 = DbExt.QueryReader(this._数据库, "SELECT * FROM 渔获最大 WHERE 鱼ID = 2313 ORDER BY 最大重量 ASC", Array.Empty<object>()))
							{
								while (queryResult44.Read())
								{
									num35 = num22;
									num22 = num35 - 1;
									flag42 = true;
									string 用户名24 = queryResult44.Get<string>("用户名");
									int 最大重量20 = queryResult44.Get<int>("最大重量");
									text22 += string.Format("[c/FFD700:{0}][c/8A2BE2:{1}]→双鳍鳕鱼重量：[c/00BFFF:{2}]  KG\n", "【第" + num22.ToString() + "名】", 用户名24, 最大重量20);
									用户名24 = null;
								}
								bool flag43 = flag42;
								bool flag330 = flag43;
								if (flag330)
								{
									text22 += "[i:2487][c/FF8C00:======↑双鳍鳕鱼重量排行↑=========][i:2487]";
									args.Player.SendSuccessMessage(text22);
									return;
								}
							}
							QueryResult queryResult44 = null;
							text22 = null;
						}
						catch (Exception ex39)
						{
							Exception ex25 = ex39;
							TShock.Log.ConsoleError(ex25.ToString());
						}
						args.Player.SendSuccessMessage("[i:2487] 还没有双鳍鳕鱼排行数据！ [i:2487]");
						goto IL_12A2E;
					}
					bool flag331 = args.Parameters[1] == "黑曜石鱼" || args.Parameters[1] == "19";
					if (flag331)
					{
						try
						{
							string text23 = "[i:2487][c/FF8C00:======↓黑曜石鱼重量排行↓=========][i:2487]\n";
							int num23 = 1;
							using (QueryResult queryResult45 = DbExt.QueryReader(this._数据库, "SELECT * FROM 渔获最大 WHERE 鱼ID = 2315 ORDER BY 最大重量 ASC", Array.Empty<object>()))
							{
								while (queryResult45.Read())
								{
									num35 = num23;
									num23 = num35 + 1;
								}
							}
							QueryResult queryResult45 = null;
							bool flag44 = false;
							using (QueryResult queryResult46 = DbExt.QueryReader(this._数据库, "SELECT * FROM 渔获最大 WHERE 鱼ID = 2315 ORDER BY 最大重量 ASC", Array.Empty<object>()))
							{
								while (queryResult46.Read())
								{
									num35 = num23;
									num23 = num35 - 1;
									flag44 = true;
									string 用户名25 = queryResult46.Get<string>("用户名");
									int 最大重量21 = queryResult46.Get<int>("最大重量");
									text23 += string.Format("[c/FFD700:{0}][c/8A2BE2:{1}]→黑曜石鱼重量：[c/00BFFF:{2}]  KG\n", "【第" + num23.ToString() + "名】", 用户名25, 最大重量21);
									用户名25 = null;
								}
								bool flag45 = flag44;
								bool flag332 = flag45;
								if (flag332)
								{
									text23 += "[i:2487][c/FF8C00:======↑黑曜石鱼重量排行↑=========][i:2487]";
									args.Player.SendSuccessMessage(text23);
									return;
								}
							}
							QueryResult queryResult46 = null;
							text23 = null;
						}
						catch (Exception ex39)
						{
							Exception ex26 = ex39;
							TShock.Log.ConsoleError(ex26.ToString());
						}
						args.Player.SendSuccessMessage("[i:2487] 还没有黑曜石鱼排行数据！ [i:2487]");
						goto IL_12A2E;
					}
					bool flag333 = args.Parameters[1] == "虾" || args.Parameters[1] == "20";
					if (flag333)
					{
						try
						{
							string text24 = "[i:2487][c/FF8C00:======↓虾重量排行↓=========][i:2487]\n";
							int num24 = 1;
							using (QueryResult queryResult47 = DbExt.QueryReader(this._数据库, "SELECT * FROM 渔获最大 WHERE 鱼ID = 2316 ORDER BY 最大重量 ASC", Array.Empty<object>()))
							{
								while (queryResult47.Read())
								{
									num35 = num24;
									num24 = num35 + 1;
								}
							}
							QueryResult queryResult47 = null;
							bool flag46 = false;
							using (QueryResult queryResult48 = DbExt.QueryReader(this._数据库, "SELECT * FROM 渔获最大 WHERE 鱼ID = 2316 ORDER BY 最大重量 ASC", Array.Empty<object>()))
							{
								while (queryResult48.Read())
								{
									num35 = num24;
									num24 = num35 - 1;
									flag46 = true;
									string 用户名26 = queryResult48.Get<string>("用户名");
									int 最大重量22 = queryResult48.Get<int>("最大重量");
									text24 += string.Format("[c/FFD700:{0}][c/8A2BE2:{1}]→虾重量：[c/00BFFF:{2}]  KG\n", "【第" + num24.ToString() + "名】", 用户名26, 最大重量22);
									用户名26 = null;
								}
								bool flag47 = flag46;
								bool flag334 = flag47;
								if (flag334)
								{
									text24 += "[i:2487][c/FF8C00:======↑虾重量排行↑=========][i:2487]";
									args.Player.SendSuccessMessage(text24);
									return;
								}
							}
							QueryResult queryResult48 = null;
							text24 = null;
						}
						catch (Exception ex39)
						{
							Exception ex27 = ex39;
							TShock.Log.ConsoleError(ex27.ToString());
						}
						args.Player.SendSuccessMessage("[i:2487] 还没有虾排行数据！ [i:2487]");
						goto IL_12A2E;
					}
					bool flag335 = args.Parameters[1] == "混沌鱼" || args.Parameters[1] == "21";
					if (flag335)
					{
						try
						{
							string text25 = "[i:2487][c/FF8C00:======↓混沌鱼重量排行↓=========][i:2487]\n";
							int num25 = 1;
							using (QueryResult queryResult49 = DbExt.QueryReader(this._数据库, "SELECT * FROM 渔获最大 WHERE 鱼ID = 2317 ORDER BY 最大重量 ASC", Array.Empty<object>()))
							{
								while (queryResult49.Read())
								{
									num35 = num25;
									num25 = num35 + 1;
								}
							}
							QueryResult queryResult49 = null;
							bool flag48 = false;
							using (QueryResult queryResult50 = DbExt.QueryReader(this._数据库, "SELECT * FROM 渔获最大 WHERE 鱼ID = 2317 ORDER BY 最大重量 ASC", Array.Empty<object>()))
							{
								while (queryResult50.Read())
								{
									num35 = num25;
									num25 = num35 - 1;
									flag48 = true;
									string 用户名27 = queryResult50.Get<string>("用户名");
									int 最大重量23 = queryResult50.Get<int>("最大重量");
									text25 += string.Format("[c/FFD700:{0}][c/8A2BE2:{1}]→混沌鱼重量：[c/00BFFF:{2}]  KG\n", "【第" + num25.ToString() + "名】", 用户名27, 最大重量23);
									用户名27 = null;
								}
								bool flag49 = flag48;
								bool flag336 = flag49;
								if (flag336)
								{
									text25 += "[i:2487][c/FF8C00:======↑混沌鱼重量排行↑=========][i:2487]";
									args.Player.SendSuccessMessage(text25);
									return;
								}
							}
							QueryResult queryResult50 = null;
							text25 = null;
						}
						catch (Exception ex39)
						{
							Exception ex28 = ex39;
							TShock.Log.ConsoleError(ex28.ToString());
						}
						args.Player.SendSuccessMessage("[i:2487] 还没有混沌鱼排行数据！ [i:2487]");
						goto IL_12A2E;
					}
					bool flag337 = args.Parameters[1] == "黑檀锦鲤" || args.Parameters[1] == "22";
					if (flag337)
					{
						try
						{
							string text26 = "[i:2487][c/FF8C00:======↓黑檀锦鲤重量排行↓=========][i:2487]\n";
							int num26 = 1;
							using (QueryResult queryResult51 = DbExt.QueryReader(this._数据库, "SELECT * FROM 渔获最大 WHERE 鱼ID = 2318 ORDER BY 最大重量 ASC", Array.Empty<object>()))
							{
								while (queryResult51.Read())
								{
									num35 = num26;
									num26 = num35 + 1;
								}
							}
							QueryResult queryResult51 = null;
							bool flag50 = false;
							using (QueryResult queryResult52 = DbExt.QueryReader(this._数据库, "SELECT * FROM 渔获最大 WHERE 鱼ID = 2318 ORDER BY 最大重量 ASC", Array.Empty<object>()))
							{
								while (queryResult52.Read())
								{
									num35 = num26;
									num26 = num35 - 1;
									flag50 = true;
									string 用户名28 = queryResult52.Get<string>("用户名");
									int 最大重量24 = queryResult52.Get<int>("最大重量");
									text26 += string.Format("[c/FFD700:{0}][c/8A2BE2:{1}]→黑檀锦鲤重量：[c/00BFFF:{2}]  KG\n", "【第" + num26.ToString() + "名】", 用户名28, 最大重量24);
									用户名28 = null;
								}
								bool flag51 = flag50;
								bool flag338 = flag51;
								if (flag338)
								{
									text26 += "[i:2487][c/FF8C00:======↑黑檀锦鲤重量排行↑=========][i:2487]";
									args.Player.SendSuccessMessage(text26);
									return;
								}
							}
							QueryResult queryResult52 = null;
							text26 = null;
						}
						catch (Exception ex39)
						{
							Exception ex29 = ex39;
							TShock.Log.ConsoleError(ex29.ToString());
						}
						args.Player.SendSuccessMessage("[i:2487] 还没有黑檀锦鲤排行数据！ [i:2487]");
						goto IL_12A2E;
					}
					bool flag339 = args.Parameters[1] == "血腥食人鱼" || args.Parameters[1] == "23";
					if (flag339)
					{
						try
						{
							string text27 = "[i:2487][c/FF8C00:======↓血腥食人鱼重量排行↓=========][i:2487]\n";
							int num27 = 1;
							using (QueryResult queryResult53 = DbExt.QueryReader(this._数据库, "SELECT * FROM 渔获最大 WHERE 鱼ID = 2319 ORDER BY 最大重量 ASC", Array.Empty<object>()))
							{
								while (queryResult53.Read())
								{
									num35 = num27;
									num27 = num35 + 1;
								}
							}
							QueryResult queryResult53 = null;
							bool flag52 = false;
							using (QueryResult queryResult54 = DbExt.QueryReader(this._数据库, "SELECT * FROM 渔获最大 WHERE 鱼ID = 2319 ORDER BY 最大重量 ASC", Array.Empty<object>()))
							{
								while (queryResult54.Read())
								{
									num35 = num27;
									num27 = num35 - 1;
									flag52 = true;
									string 用户名29 = queryResult54.Get<string>("用户名");
									int 最大重量25 = queryResult54.Get<int>("最大重量");
									text27 += string.Format("[c/FFD700:{0}][c/8A2BE2:{1}]→血腥食人鱼重量：[c/00BFFF:{2}]  KG\n", "【第" + num27.ToString() + "名】", 用户名29, 最大重量25);
									用户名29 = null;
								}
								bool flag53 = flag52;
								bool flag340 = flag53;
								if (flag340)
								{
									text27 += "[i:2487][c/FF8C00:======↑血腥食人鱼重量排行↑=========][i:2487]";
									args.Player.SendSuccessMessage(text27);
									return;
								}
							}
							QueryResult queryResult54 = null;
							text27 = null;
						}
						catch (Exception ex39)
						{
							Exception ex30 = ex39;
							TShock.Log.ConsoleError(ex30.ToString());
						}
						args.Player.SendSuccessMessage("[i:2487] 还没有血腥食人鱼排行数据！ [i:2487]");
						goto IL_12A2E;
					}
					bool flag341 = args.Parameters[1] == "臭味鱼" || args.Parameters[1] == "24";
					if (flag341)
					{
						try
						{
							string text28 = "[i:2487][c/FF8C00:======↓臭味鱼重量排行↓=========][i:2487]\n";
							int num28 = 1;
							using (QueryResult queryResult55 = DbExt.QueryReader(this._数据库, "SELECT * FROM 渔获最大 WHERE 鱼ID = 2321 ORDER BY 最大重量 ASC", Array.Empty<object>()))
							{
								while (queryResult55.Read())
								{
									num35 = num28;
									num28 = num35 + 1;
								}
							}
							QueryResult queryResult55 = null;
							bool flag54 = false;
							using (QueryResult queryResult56 = DbExt.QueryReader(this._数据库, "SELECT * FROM 渔获最大 WHERE 鱼ID = 2321 ORDER BY 最大重量 ASC", Array.Empty<object>()))
							{
								while (queryResult56.Read())
								{
									num35 = num28;
									num28 = num35 - 1;
									flag54 = true;
									string 用户名30 = queryResult56.Get<string>("用户名");
									int 最大重量26 = queryResult56.Get<int>("最大重量");
									text28 += string.Format("[c/FFD700:{0}][c/8A2BE2:{1}]→臭味鱼重量：[c/00BFFF:{2}]  KG\n", "【第" + num28.ToString() + "名】", 用户名30, 最大重量26);
									用户名30 = null;
								}
								bool flag55 = flag54;
								bool flag342 = flag55;
								if (flag342)
								{
									text28 += "[i:2487][c/FF8C00:======↑臭味鱼重量排行↑=========][i:2487]";
									args.Player.SendSuccessMessage(text28);
									return;
								}
							}
							QueryResult queryResult56 = null;
							text28 = null;
						}
						catch (Exception ex39)
						{
							Exception ex31 = ex39;
							TShock.Log.ConsoleError(ex31.ToString());
						}
						args.Player.SendSuccessMessage("[i:2487] 还没有臭味鱼排行数据！ [i:2487]");
						goto IL_12A2E;
					}
					bool flag343 = args.Parameters[1] == "蓝水母" || args.Parameters[1] == "25";
					if (flag343)
					{
						try
						{
							string text29 = "[i:2487][c/FF8C00:======↓蓝水母重量排行↓=========][i:2487]\n";
							int num29 = 1;
							using (QueryResult queryResult57 = DbExt.QueryReader(this._数据库, "SELECT * FROM 渔获最大 WHERE 鱼ID = 2436 ORDER BY 最大重量 ASC", Array.Empty<object>()))
							{
								while (queryResult57.Read())
								{
									num35 = num29;
									num29 = num35 + 1;
								}
							}
							QueryResult queryResult57 = null;
							bool flag56 = false;
							using (QueryResult queryResult58 = DbExt.QueryReader(this._数据库, "SELECT * FROM 渔获最大 WHERE 鱼ID = 2436 ORDER BY 最大重量 ASC", Array.Empty<object>()))
							{
								while (queryResult58.Read())
								{
									num35 = num29;
									num29 = num35 - 1;
									flag56 = true;
									string 用户名31 = queryResult58.Get<string>("用户名");
									int 最大重量27 = queryResult58.Get<int>("最大重量");
									text29 += string.Format("[c/FFD700:{0}][c/8A2BE2:{1}]→蓝水母重量：[c/00BFFF:{2}]  KG\n", "【第" + num29.ToString() + "名】", 用户名31, 最大重量27);
									用户名31 = null;
								}
								bool flag57 = flag56;
								bool flag344 = flag57;
								if (flag344)
								{
									text29 += "[i:2487][c/FF8C00:======↑蓝水母重量排行↑=========][i:2487]";
									args.Player.SendSuccessMessage(text29);
									return;
								}
							}
							QueryResult queryResult58 = null;
							text29 = null;
						}
						catch (Exception ex39)
						{
							Exception ex32 = ex39;
							TShock.Log.ConsoleError(ex32.ToString());
						}
						args.Player.SendSuccessMessage("[i:2487] 还没有蓝水母排行数据！ [i:2487]");
						goto IL_12A2E;
					}
					bool flag345 = args.Parameters[1] == "绿水母" || args.Parameters[1] == "26";
					if (flag345)
					{
						try
						{
							string text30 = "[i:2487][c/FF8C00:======↓绿水母重量排行↓=========][i:2487]\n";
							int num30 = 1;
							using (QueryResult queryResult59 = DbExt.QueryReader(this._数据库, "SELECT * FROM 渔获最大 WHERE 鱼ID = 2437 ORDER BY 最大重量 ASC", Array.Empty<object>()))
							{
								while (queryResult59.Read())
								{
									num35 = num30;
									num30 = num35 + 1;
								}
							}
							QueryResult queryResult59 = null;
							bool flag58 = false;
							using (QueryResult queryResult60 = DbExt.QueryReader(this._数据库, "SELECT * FROM 渔获最大 WHERE 鱼ID = 2437 ORDER BY 最大重量 ASC", Array.Empty<object>()))
							{
								while (queryResult60.Read())
								{
									num35 = num30;
									num30 = num35 - 1;
									flag58 = true;
									string 用户名32 = queryResult60.Get<string>("用户名");
									int 最大重量28 = queryResult60.Get<int>("最大重量");
									text30 += string.Format("[c/FFD700:{0}][c/8A2BE2:{1}]→绿水母重量：[c/00BFFF:{2}]  KG\n", "【第" + num30.ToString() + "名】", 用户名32, 最大重量28);
									用户名32 = null;
								}
								bool flag59 = flag58;
								bool flag346 = flag59;
								if (flag346)
								{
									text30 += "[i:2487][c/FF8C00:======↑绿水母重量排行↑=========][i:2487]";
									args.Player.SendSuccessMessage(text30);
									return;
								}
							}
							QueryResult queryResult60 = null;
							text30 = null;
						}
						catch (Exception ex39)
						{
							Exception ex33 = ex39;
							TShock.Log.ConsoleError(ex33.ToString());
						}
						args.Player.SendSuccessMessage("[i:2487] 还没有绿水母排行数据！ [i:2487]");
						goto IL_12A2E;
					}
					bool flag347 = args.Parameters[1] == "粉水母" || args.Parameters[1] == "27";
					if (flag347)
					{
						try
						{
							string text31 = "[i:2487][c/FF8C00:======↓粉水母重量排行↓=========][i:2487]\n";
							int num31 = 1;
							using (QueryResult queryResult61 = DbExt.QueryReader(this._数据库, "SELECT * FROM 渔获最大 WHERE 鱼ID = 2438 ORDER BY 最大重量 ASC", Array.Empty<object>()))
							{
								while (queryResult61.Read())
								{
									num35 = num31;
									num31 = num35 + 1;
								}
							}
							QueryResult queryResult61 = null;
							bool flag60 = false;
							using (QueryResult queryResult62 = DbExt.QueryReader(this._数据库, "SELECT * FROM 渔获最大 WHERE 鱼ID = 2438 ORDER BY 最大重量 ASC", Array.Empty<object>()))
							{
								while (queryResult62.Read())
								{
									num35 = num31;
									num31 = num35 - 1;
									flag60 = true;
									string 用户名33 = queryResult62.Get<string>("用户名");
									int 最大重量29 = queryResult62.Get<int>("最大重量");
									text31 += string.Format("[c/FFD700:{0}][c/8A2BE2:{1}]→粉水母重量：[c/00BFFF:{2}]  KG\n", "【第" + num31.ToString() + "名】", 用户名33, 最大重量29);
									用户名33 = null;
								}
								bool flag61 = flag60;
								bool flag348 = flag61;
								if (flag348)
								{
									text31 += "[i:2487][c/FF8C00:======↑粉水母重量排行↑=========][i:2487]";
									args.Player.SendSuccessMessage(text31);
									return;
								}
							}
							QueryResult queryResult62 = null;
							text31 = null;
						}
						catch (Exception ex39)
						{
							Exception ex34 = ex39;
							TShock.Log.ConsoleError(ex34.ToString());
						}
						args.Player.SendSuccessMessage("[i:2487] 还没有粉水母排行数据！ [i:2487]");
						goto IL_12A2E;
					}
					bool flag349 = args.Parameters[1] == "偏口鱼" || args.Parameters[1] == "28";
					if (flag349)
					{
						try
						{
							string text32 = "[i:2487][c/FF8C00:======↓偏口鱼重量排行↓=========][i:2487]\n";
							int num32 = 1;
							using (QueryResult queryResult63 = DbExt.QueryReader(this._数据库, "SELECT * FROM 渔获最大 WHERE 鱼ID = 4401 ORDER BY 最大重量 ASC", Array.Empty<object>()))
							{
								while (queryResult63.Read())
								{
									num35 = num32;
									num32 = num35 + 1;
								}
							}
							QueryResult queryResult63 = null;
							bool flag62 = false;
							using (QueryResult queryResult64 = DbExt.QueryReader(this._数据库, "SELECT * FROM 渔获最大 WHERE 鱼ID = 4401 ORDER BY 最大重量 ASC", Array.Empty<object>()))
							{
								while (queryResult64.Read())
								{
									num35 = num32;
									num32 = num35 - 1;
									flag62 = true;
									string 用户名34 = queryResult64.Get<string>("用户名");
									int 最大重量30 = queryResult64.Get<int>("最大重量");
									text32 += string.Format("[c/FFD700:{0}][c/8A2BE2:{1}]→偏口鱼重量：[c/00BFFF:{2}]  KG\n", "【第" + num32.ToString() + "名】", 用户名34, 最大重量30);
									用户名34 = null;
								}
								bool flag63 = flag62;
								bool flag350 = flag63;
								if (flag350)
								{
									text32 += "[i:2487][c/FF8C00:======↑偏口鱼重量排行↑=========][i:2487]";
									args.Player.SendSuccessMessage(text32);
									return;
								}
							}
							QueryResult queryResult64 = null;
							text32 = null;
						}
						catch (Exception ex39)
						{
							Exception ex35 = ex39;
							TShock.Log.ConsoleError(ex35.ToString());
						}
						args.Player.SendSuccessMessage("[i:2487] 还没有偏口鱼排行数据！ [i:2487]");
						goto IL_12A2E;
					}
					bool flag351 = args.Parameters[1] == "岩龙虾" || args.Parameters[1] == "29";
					if (flag351)
					{
						try
						{
							string text33 = "[i:2487][c/FF8C00:======↓岩龙虾重量排行↓=========][i:2487]\n";
							int num33 = 1;
							using (QueryResult queryResult65 = DbExt.QueryReader(this._数据库, "SELECT * FROM 渔获最大 WHERE 鱼ID = 4402 ORDER BY 最大重量 ASC", Array.Empty<object>()))
							{
								while (queryResult65.Read())
								{
									num35 = num33;
									num33 = num35 + 1;
								}
							}
							QueryResult queryResult65 = null;
							bool flag64 = false;
							using (QueryResult queryResult66 = DbExt.QueryReader(this._数据库, "SELECT * FROM 渔获最大 WHERE 鱼ID = 4402 ORDER BY 最大重量 ASC", Array.Empty<object>()))
							{
								while (queryResult66.Read())
								{
									num35 = num33;
									num33 = num35 - 1;
									flag64 = true;
									string 用户名35 = queryResult66.Get<string>("用户名");
									int 最大重量31 = queryResult66.Get<int>("最大重量");
									text33 += string.Format("[c/FFD700:{0}][c/8A2BE2:{1}]→岩龙虾重量：[c/00BFFF:{2}]  KG\n", "【第" + num33.ToString() + "名】", 用户名35, 最大重量31);
									用户名35 = null;
								}
								bool flag65 = flag64;
								bool flag352 = flag65;
								if (flag352)
								{
									text33 += "[i:2487][c/FF8C00:======↑岩龙虾重量排行↑=========][i:2487]";
									args.Player.SendSuccessMessage(text33);
									return;
								}
							}
							QueryResult queryResult66 = null;
							text33 = null;
						}
						catch (Exception ex39)
						{
							Exception ex36 = ex39;
							TShock.Log.ConsoleError(ex36.ToString());
						}
						args.Player.SendSuccessMessage("[i:2487] 还没有岩龙虾排行数据！ [i:2487]");
						goto IL_12A2E;
					}
				}
				catch (Exception ex39)
				{
					Exception ex37 = ex39;
					TShock.Log.ConsoleError(ex37.ToString());
				}
				args.Player.SendErrorMessage("[i:2487] 指令错误，正确指令：/渔获 排行 [总数/重量/大小/鱼序号]\r\n[i:2487]鱼序号如下： \r\n[i:2487] 1=[i:2290] 2=[i:2297] 3=[i:2298] 4=[i:2299] 5=[i:2300] \r\n[i:2487] 6=[i:2301] 7=[i:2302] 8=[i:2303] 9=[i:2304] 10=[i:2305] 11=[i:2306] \r\n[i:2487] 12=[i:2307] 13=[i:2308] 14=[i:2309] 15=[i:2310] 16=[i:2311] 17=[i:2312] \r\n[i:2487] 18=[i:2313] 19=[i:2315] 20=[i:2316] 21=[i:2317] 22=[i:2318] 23=[i:2319] \r\n[i:2487] 24=[i:2321] 25=[i:2436] 26=[i:2437] 27=[i:2438] 28=[i:4401] 29=[i:4402]  ");
				args.Player.SendInfoMessage("[i:2487] 帮助：输入鱼前面的数字查看鱼排名，如：/渔获 排行 10 ");
				goto IL_12A2E;
				IL_11F27:
				bool flag353 = !args.Player.HasPermission("gofishing.game");
				if (flag353)
				{
					args.Player.SendErrorMessage("[i:2487] 你没有开启钓鱼竞赛活动的权限！ [i:2487]");
					return;
				}
				try
				{
					string 取竞赛时长 = args.Parameters[1];
					string s = 取竞赛时长;
					int 时长_分 = Convert.ToInt32(s) * 60000;
					TSPlayer[] 用户 = TShock.Players;
					bool flag354 = !"".Equals(取竞赛时长);
					if (flag354)
					{
						string text34 = "/bc [i:2487]钓鱼竞赛活动已经开始，为期" + 取竞赛时长 + "分钟，请在倒计时结束前回收鱼，否则数据不参与竞赛!";
						string text35 = "/bc [i:2487]钓鱼竞赛活动还有1分钟结束!";
						string text36 = "/bc [i:2487]钓鱼竞赛活动倒计时：10秒!";
						string text37 = "/bc [i:2487]钓鱼竞赛活动倒计时：9秒!";
						string text38 = "/bc [i:2487]钓鱼竞赛活动倒计时：8秒!";
						string text39 = "/bc [i:2487]钓鱼竞赛活动倒计时：7秒!";
						string text40 = "/bc [i:2487]钓鱼竞赛活动倒计时：6秒!";
						string text41 = "/bc [i:2487]钓鱼竞赛活动倒计时：5秒!";
						string text42 = "/bc [i:2487]钓鱼竞赛活动倒计时：4秒!";
						string text43 = "/bc [i:2487]钓鱼竞赛活动倒计时：3秒!";
						string text44 = "/bc [i:2487]钓鱼竞赛活动倒计时：2秒!";
						string text45 = "/bc [i:2487]钓鱼竞赛活动倒计时：1秒!";
						string text46 = "/bc [i:2487]钓鱼竞赛活动结束，现在公布结果!";
						Commands.HandleCommand(TSPlayer.Server, text34);
						DbExt.Query(TShock.DB, "UPDATE 渔获统计 SET 竞赛分 = @0 ", new object[] { 0 });
						DbExt.Query(TShock.DB, "UPDATE 渔获统计 SET 竞赛排名 = @0 ", new object[] { 0 });
						args.Player.SendSuccessMessage("[i:2487] 已重置所有玩家的竞赛分、竞赛排名 [i:2487]");
						await Task.Delay(时长_分 - 60000);
						Commands.HandleCommand(TSPlayer.Server, text35);
						await Task.Delay(50000);
						Commands.HandleCommand(TSPlayer.Server, text36);
						await Task.Delay(1000);
						Commands.HandleCommand(TSPlayer.Server, text37);
						await Task.Delay(1000);
						Commands.HandleCommand(TSPlayer.Server, text38);
						await Task.Delay(1000);
						Commands.HandleCommand(TSPlayer.Server, text39);
						await Task.Delay(1000);
						Commands.HandleCommand(TSPlayer.Server, text40);
						await Task.Delay(1000);
						Commands.HandleCommand(TSPlayer.Server, text41);
						await Task.Delay(1000);
						Commands.HandleCommand(TSPlayer.Server, text42);
						await Task.Delay(1000);
						Commands.HandleCommand(TSPlayer.Server, text43);
						await Task.Delay(1000);
						Commands.HandleCommand(TSPlayer.Server, text44);
						await Task.Delay(1000);
						Commands.HandleCommand(TSPlayer.Server, text45);
						await Task.Delay(1000);
						Commands.HandleCommand(TSPlayer.Server, text46);
						await Task.Delay(200);
						DbExt.Query(TShock.DB, "UPDATE 渔获统计 SET 竞赛排名 =  @0  ", new object[] { 1 });
						await Task.Delay(200);
						this.竞赛结果(args);
						goto IL_12A2E;
					}
					取竞赛时长 = null;
					s = null;
				}
				catch (Exception ex39)
				{
					Exception ex38 = ex39;
					TShock.Log.ConsoleError(ex38.ToString());
				}
				args.Player.SendErrorMessage("[i:2487] 指令错误，正确指令：/渔获 竞赛 [时长/分] [i:2487]");
				IL_12A2E:
				text48 = null;
			}
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002818 File Offset: 0x00000A18
		private void GetData(GetDataEventArgs args)
		{
			TSPlayer tsplayer = TShock.Players[args.Msg.whoAmI];
			string text = DateTime.Now.ToString("HH:mm:ss");
			int 定时竞赛时长_分钟 = Config.GetConfig().定时竞赛时长_分钟;
			string text2 = string.Format("/渔获 竞赛 {0}", 定时竞赛时长_分钟);
			bool flag = Config.GetConfig().竞赛时间1 == text || Config.GetConfig().竞赛时间2 == text || Config.GetConfig().竞赛时间3 == text || Config.GetConfig().竞赛时间4 == text || Config.GetConfig().竞赛时间5 == text || Config.GetConfig().竞赛时间6 == text || Config.GetConfig().竞赛时间7 == text || Config.GetConfig().竞赛时间8 == text;
			if (flag)
			{
				Commands.HandleCommand(TSPlayer.Server, text2);
			}
			bool handled = args.Handled;
			bool flag2 = !handled;
			if (flag2)
			{
				bool flag3 = tsplayer != null;
				if (flag3)
				{
					bool connectionAlive = tsplayer.ConnectionAlive;
					if (connectionAlive)
					{
						bool flag4 = args.MsgID == 13;
						if (flag4)
						{
							using (MemoryStream memoryStream = new MemoryStream(args.Msg.readBuffer, args.Index, args.Length))
							{
								int num = memoryStream.ReadByte();
								ControlSet controlSet;
								controlSet..ctor((byte)memoryStream.ReadByte());
								bool isUsingItem = controlSet.IsUsingItem;
								if (isUsingItem)
								{
									List<int> list = new List<int>
									{
										2290, 2297, 2298, 2299, 2300, 2301, 2302, 2303, 2304, 2305,
										2306, 2307, 2308, 2309, 2310, 2311, 2312, 2313, 2315, 2316,
										2317, 2318, 2319, 2321, 2436, 2437, 2438, 4401, 4402
									};
									bool flag5 = list.Contains(tsplayer.SelectedItem.netID);
									bool flag6 = flag5;
									if (flag6)
									{
										Commands.HandleCommand(tsplayer, string.Format("/渔获 鉴定回收", Array.Empty<object>()));
									}
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002B70 File Offset: 0x00000D70
		private void 竞赛结果(CommandArgs args)
		{
			try
			{
				string text = "/bc [i:2487][c/FF8C00:== 竞赛排行 ==][i:2487]\r\n";
				int num = 1;
				using (QueryResult queryResult = DbExt.QueryReader(this._数据库, "SELECT * FROM 渔获统计 ORDER BY 竞赛分 ASC", Array.Empty<object>()))
				{
					while (queryResult.Read())
					{
						num++;
					}
				}
				bool flag = false;
				using (QueryResult queryResult2 = DbExt.QueryReader(this._数据库, "SELECT * FROM 渔获统计 ORDER BY 竞赛分 ASC", Array.Empty<object>()))
				{
					while (queryResult2.Read())
					{
						num--;
						flag = true;
						string text2 = queryResult2.Get<string>("用户名");
						int num2 = queryResult2.Get<int>("竞赛分");
						text += string.Format("[c/FFD700:{0}][c/FF1493:{1}] 得分：[c/00BFFF:{2}] 尾...\r\n", "【第" + num.ToString() + "名】", text2, num2);
					}
					bool flag2 = flag;
					bool flag3 = flag2;
					if (flag3)
					{
						text += "[c/FF8C00:恭喜前一名玩家，领奖请输入/渔获 领奖，如果没有获得奖励，请喊群主！]";
						Commands.HandleCommand(TSPlayer.Server, text);
						return;
					}
				}
			}
			catch (Exception ex)
			{
				TShock.Log.ConsoleError(ex.ToString());
			}
			args.Player.SendSuccessMessage("[i:2487] 当前服务器没有玩家渔获记录 [i:2487]");
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002CC8 File Offset: 0x00000EC8
		private void PlayItemSet(int ID, int slot, int Item, int stack)
		{
			TSPlayer tsplayer = new TSPlayer(ID);
			Item itemById = TShock.Utils.GetItemById(Item);
			itemById.stack = stack;
			bool flag = slot < NetItem.InventorySlots;
			bool flag2 = flag;
			bool flag3 = flag2;
			if (flag3)
			{
				tsplayer.TPlayer.inventory[slot] = itemById;
				NetMessage.SendData(5, -1, -1, NetworkText.FromLiteral(tsplayer.TPlayer.inventory[slot].Name), tsplayer.Index, (float)slot, (float)tsplayer.TPlayer.inventory[slot].prefix, 0f, 0, 0, 0);
				NetMessage.SendData(5, tsplayer.Index, -1, NetworkText.FromLiteral(tsplayer.TPlayer.inventory[slot].Name), tsplayer.Index, (float)slot, (float)tsplayer.TPlayer.inventory[slot].prefix, 0f, 0, 0, 0);
			}
			else
			{
				bool flag4 = slot < NetItem.InventorySlots + NetItem.ArmorSlots;
				bool flag5 = flag4;
				bool flag6 = flag5;
				if (flag6)
				{
					int num = slot - NetItem.InventorySlots;
					tsplayer.TPlayer.armor[num] = itemById;
					NetMessage.SendData(5, -1, -1, NetworkText.FromLiteral(tsplayer.TPlayer.armor[num].Name), tsplayer.Index, (float)slot, (float)tsplayer.TPlayer.armor[num].prefix, 0f, 0, 0, 0);
					NetMessage.SendData(5, tsplayer.Index, -1, NetworkText.FromLiteral(tsplayer.TPlayer.armor[num].Name), tsplayer.Index, (float)slot, (float)tsplayer.TPlayer.armor[num].prefix, 0f, 0, 0, 0);
				}
				else
				{
					bool flag7 = slot < NetItem.InventorySlots + NetItem.ArmorSlots + NetItem.DyeSlots;
					bool flag8 = flag7;
					bool flag9 = flag8;
					if (flag9)
					{
						int num2 = slot - (NetItem.InventorySlots + NetItem.ArmorSlots);
						tsplayer.TPlayer.dye[num2] = itemById;
						NetMessage.SendData(5, -1, -1, NetworkText.FromLiteral(tsplayer.TPlayer.dye[num2].Name), tsplayer.Index, (float)slot, (float)tsplayer.TPlayer.dye[num2].prefix, 0f, 0, 0, 0);
						NetMessage.SendData(5, tsplayer.Index, -1, NetworkText.FromLiteral(tsplayer.TPlayer.dye[num2].Name), tsplayer.Index, (float)slot, (float)tsplayer.TPlayer.dye[num2].prefix, 0f, 0, 0, 0);
					}
					else
					{
						bool flag10 = slot < NetItem.InventorySlots + NetItem.ArmorSlots + NetItem.DyeSlots + NetItem.MiscEquipSlots;
						bool flag11 = flag10;
						bool flag12 = flag11;
						if (flag12)
						{
							int num3 = slot - (NetItem.InventorySlots + NetItem.ArmorSlots + NetItem.DyeSlots);
							tsplayer.TPlayer.miscEquips[num3] = itemById;
							NetMessage.SendData(5, -1, -1, NetworkText.FromLiteral(tsplayer.TPlayer.miscEquips[num3].Name), tsplayer.Index, (float)slot, (float)tsplayer.TPlayer.miscEquips[num3].prefix, 0f, 0, 0, 0);
							NetMessage.SendData(5, tsplayer.Index, -1, NetworkText.FromLiteral(tsplayer.TPlayer.miscEquips[num3].Name), tsplayer.Index, (float)slot, (float)tsplayer.TPlayer.miscEquips[num3].prefix, 0f, 0, 0, 0);
						}
						else
						{
							bool flag13 = slot < NetItem.InventorySlots + NetItem.ArmorSlots + NetItem.DyeSlots + NetItem.MiscEquipSlots + NetItem.MiscDyeSlots;
							bool flag14 = flag13;
							bool flag15 = flag14;
							if (flag15)
							{
								int num4 = slot - (NetItem.InventorySlots + NetItem.ArmorSlots + NetItem.DyeSlots + NetItem.MiscEquipSlots);
								tsplayer.TPlayer.miscDyes[num4] = itemById;
								NetMessage.SendData(5, -1, -1, NetworkText.FromLiteral(tsplayer.TPlayer.miscDyes[num4].Name), tsplayer.Index, (float)slot, (float)tsplayer.TPlayer.miscDyes[num4].prefix, 0f, 0, 0, 0);
								NetMessage.SendData(5, tsplayer.Index, -1, NetworkText.FromLiteral(tsplayer.TPlayer.miscDyes[num4].Name), tsplayer.Index, (float)slot, (float)tsplayer.TPlayer.miscDyes[num4].prefix, 0f, 0, 0, 0);
							}
						}
					}
				}
			}
		}

		// Token: 0x0400000F RID: 15
		private IDbConnection _数据库;

		// Token: 0x04000010 RID: 16
		private const string Permission = "gofishing.game";

		// Token: 0x02000006 RID: 6
		public class Item1
		{
			// Token: 0x17000008 RID: 8
			// (get) Token: 0x06000016 RID: 22 RVA: 0x00003162 File Offset: 0x00001362
			// (set) Token: 0x06000017 RID: 23 RVA: 0x0000316A File Offset: 0x0000136A
			public int NetID { get; set; } = 0;

			// Token: 0x17000009 RID: 9
			// (get) Token: 0x06000018 RID: 24 RVA: 0x00003173 File Offset: 0x00001373
			// (set) Token: 0x06000019 RID: 25 RVA: 0x0000317B File Offset: 0x0000137B
			public int Stack { get; set; } = 0;

			// Token: 0x1700000A RID: 10
			// (get) Token: 0x0600001A RID: 26 RVA: 0x00003184 File Offset: 0x00001384
			// (set) Token: 0x0600001B RID: 27 RVA: 0x0000318C File Offset: 0x0000138C
			public int Perfix { get; set; } = 0;
		}
	}
}
