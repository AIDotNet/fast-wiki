namespace FastWiki.Web.Rcl.Pages.Dashboard
{
    public partial class Analytics : ProComponentBase
    {
        private object? _subscribersChart;
        private object? _ordersChart;
        private object? _sessionsChart;
        private object? _salesChart;

        protected override void OnInitialized()
        {
            _subscribersChart = new
            {
                Tooltip = new
                {
                    trigger = "axis",
                },
                XAxis = new
                {
                    axisLine = new
                    {
                        Show = false
                    },
                    axisLabel = new
                    {
                        Show = false
                    },
                    axisTick = new
                    {
                        Show = false
                    },
                    splitLine = new
                    {
                        Show = false
                    },
                    Data = new[]
                    {
                        "","", "","","","",""
                    }
                },
                YAxis = new
                {
                    axisLine = new
                    {
                        Show = false
                    },
                    axisLabel = new
                    {
                        Show = false
                    },
                    axisTick = new
                    {
                        Show = false
                    },
                    splitLine = new
                    {
                        Show = false
                    },
                },
                Series = new[]
                 {
                    new
                    {
                        Type = "line",
                        Data = AnalyticsService.GetSubscribersChartData(),
                        Color = "#7367f0",
                        Smooth = true,
                        SymbolSize = 10,
                        Symbol = "circle",
                        ShowSymbol = false,
                        LineStyle = new
                        {
                            Width=3
                        },
                        AreaStyle = new
                        {
                            Color = new
                            {
                                Type="linear",
                                x=0,y=0,x2=0,y2=1,
                                ColorStops=new[]
                                {
                                    new {offset = 0.1, Color = "rgb(224,222,253)"},
                                    new {offset = 0.4, Color = "rgb(255,255,255)"},
                                },
                                Global=false
                            }

                        }
                    }
                },
                Grid = new
                {
                    X = -30,
                    y = 6,
                    x2 = -30,
                    y2 = -50
                }
            };

            _ordersChart = new
            {
                Tooltip = new
                {
                    trigger = "axis",
                },
                XAxis = new
                {
                    axisLine = new
                    {
                        Show = false
                    },
                    axisLabel = new
                    {
                        Show = false
                    },
                    axisTick = new
                    {
                        Show = false
                    },
                    splitLine = new
                    {
                        Show = false
                    },
                    Data = new[]
                    {
                        "","", "","","","",""
                    }
                },
                YAxis = new
                {
                    axisLine = new
                    {
                        Show = false
                    },
                    axisLabel = new
                    {
                        Show = false
                    },
                    axisTick = new
                    {
                        Show = false
                    },
                    splitLine = new
                    {
                        Show = false
                    },
                },
                Series = new[]
                {
                    new
                    {
                        Name="Orders",
                        Type= "line",
                        Data= AnalyticsService.GetOrdersChartData(),
                        Color="#ff9f43",
                        Smooth=true,
                        SymbolSize=10,
                        Symbol="circle",
                        ShowSymbol=false,
                        LineStyle=new
                        {
                            Width = 3
                        },
                        AreaStyle = new
                        {
                            Color = new
                            {
                                Type = "linear",
                                x = 0,y = 0,x2 = 0,y2 = 1,
                                ColorStops = new[]
                                {
                                    new {offset = 0.1, Color = "rgb(255,238,222)"},
                                    new {offset = 0.4, Color = "rgb(255,255,255)"},
                                },
                                Global = false
                            }

                        }
                    }
                },
                Grid = new
                {
                    X = -30,
                    y = 6,
                    x2 = -30,
                    y2 = -60
                }
            };

            _sessionsChart = new
            {
                Tooltip = new
                {
                    Trigger = "axis"
                },
                XAxis = new
                {
                    Data = new[] { "", "", "", "", "", "", "" },
                    axisLine = new
                    {
                        Show = false
                    },
                    axisLabel = new
                    {
                        Show = false
                    },
                    axisTick = new
                    {
                        Show = false
                    },
                    splitLine = new
                    {
                        Show = false
                    },
                },
                YAxis = new
                {
                    axisLine = new
                    {
                        Show = false
                    },
                    axisLabel = new
                    {
                        Show = false
                    },
                    axisTick = new
                    {
                        Show = false
                    },
                    splitLine = new
                    {
                        Show = false
                    },
                },
                Series = new[]
                {
                    new
                    {
                        Name ="Sessions",
                        Type = "bar",
                        Data = AnalyticsService.GetSessionsChartData(),
                        Colo = "#7367f0",
                        Stack = "x",
                        BarWidth = "25",
                        ItemStyle = new
                        {
                            Normal = new
                            {
                                 BarBorderRadius = new []{ 15, 15, 0, 0}
                            }
                        }
                    },
                },
                Grid = new
                {
                    X = 0,
                    y = 0,
                    x2 = 0,
                    y2 = 0
                }
            };

            var _salesChartData = AnalyticsService.GetSalesChartData();

            _salesChart = new
            {
                Tooltip = new
                {
                    trigger = "axis",
                },
                Legend = new
                {
                    Left = "Left",
                    Data = new[]
                    {
                        "Sales","Visits"
                    }
                },
                Radar = new[]
                {
                    new
                    {
                        Indicator=new []
                        {
                            new {Text="Jan",Max=100 },
                            new {Text="Jun",Max=100 },
                            new {Text="May",Max=100 },
                            new {Text="Apr",Max=100 },
                            new {Text="Mar",Max=100 },
                            new {Text="Feb",Max=100 },
                        },
                        Center = new []{"50%","60%"},
                        Radius = 80,
                    }
                },
                Series = new[]
                {
                  new
                  {
                       Type= "radar",
                       Data= new[]
                       {
                           new
                           {
                               Value = _salesChartData[0],
                               Name= "Sales"
                           }
                       },
                       Color = "#7367f0",
                       areaStyle = new  {},
                       SymbolSize = 1,
                       Symbol = "circle",
                       ShowSymbol = false,
                  },
                  new
                  {
                       Type= "radar",
                       Data= new[]
                       {
                           new
                           {
                               Value = _salesChartData[1],
                               Name = "Visits"
                           }
                       },
                       Color = "#00BCD4",
                       areaStyle = new  {},
                       SymbolSize = 1,
                       Symbol = "circle",
                       ShowSymbol = false,
                  }
                },
                Grid = new
                {
                    X = 0,
                    y = 0,
                    x2 = 0,
                    y2 = 0
                }
            };

        }

        private StringNumber? _lastDate;

        private static Dictionary<string, object> MerginAttributes(Dictionary<string, object> attributes, Dictionary<string, object> otherAttributes)
        {
            foreach (var (key, value) in otherAttributes)
            {
                if (attributes.ContainsKey(key) is false) attributes.Add(key, value);
            }
            return attributes;
        }
    }
}
