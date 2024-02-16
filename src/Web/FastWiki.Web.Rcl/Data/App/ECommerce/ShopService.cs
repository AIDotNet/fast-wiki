namespace Masa.Blazor.Pro.Data.App.ECommerce;

public class ShopService
{
    static List<GoodsDto> _datas = new ()
    {
        new("GA506B 温热管线饮水机",239.99, "/img/apps-eCommerce/15.jpg", "饮水机",5,"LONSID","独特内胆 省电节能 智能触控 时尚科技 人性化操作 界面易懂 电子童锁实用安全 水电自动分离 停水贴心保护。不锈钢内胆：永不生锈，加热快，健康安全、彩灯显示：彩色灯条显示，让喝水成为一种享受。自动停水：连续出水超过1分钟会自动停止出水，防止中途离开出现意外。"),
        new("朗诗德G3速热管线机",339.99, "img/apps-eCommerce/18.jpg", "饮水机",5,"LONSID","六重智能防护 高原模式 精准控温 硅胶密封设计 休眠模式。适配全通量纯水机：适配全通量纯水机没有频繁起跳的环境噪音，有桶、无桶纯水机都适用。即热即饮：采用速热技术，一次沸腾，避免千滚水。六档精准定制水温：进出水双NTC+可控硅调节精准控温，可满足六种水温需求：25℃常温、55℃泡奶、55℃暖胃温开水、75℃花茶、85℃红茶、95℃咖啡、自动休眠：60s内无任何操作，自动进入休眠模式，节能省电，夜晚不打扰、大小杯设置：小杯 150ml、大杯500ml，走开也能放心接。"),
        new("朗诗德牌厨下热饮机",139.99, "img/apps-eCommerce/19.jpg", "饮水机",5,"LONSID","纯净热饮，触手可得 多重用水防护，守护家人安全 智能灯环龙头，水温状态一目了然 厨下安装更省空间，体积小更美观 贴心童锁，防止烫伤 高原模式，轻松设置。净享热饮：告别传统烧水模式，打开龙头即可享受纯净热水,四档温度可供选择，满足家庭日常用水需求。多用重水防护：干烧保护，加热超时保护，缺水保护，守护家人安全。智能灯环龙头：常温水、热水对应不同灯环颜色。厨下安装更省空间:体积小更美观。"),
        new("G1管线饮水机",539.99, "img/apps-eCommerce/16.jpg", "饮水机",5,"LONSID","采用热胆加热方式 出水速度更快 选用优质热胆保温棉 PU材质 选用涂三防漆的PCB板 醒目的琴键式按压开关 可靠性高 可拆卸接水盒 清洗更方便。双重防干烧：温度过高、缺水烧水时，自动停止运行。随时掌握工作状态：动态彩灯实时显示，随时了解工作状态。琴键按压：醒目的琴键式按压开关，可靠性高，红色按压出热水，蓝色出温水。"),
        new("GT3桌面即热饮水机",539.99, "img/apps-eCommerce/17.jpg", "饮水机",5,"LONSID","童锁 防干烧 多重安全保护 五档温度 选择不同出水温度 3秒即热杜绝反复加热 3.2L大水箱满足全天候饮水需求 旋钮调温 取水操作方便。操作简便：旋钮调温取水，使用只需一旋一按，省去复杂操作步骤，简单易上手。3秒即热 ：3秒即热，杜绝反复加热，畅饮鲜活水。多重保护：童锁、三重防干烧、缺水保护和智能缺水提醒，安全使用更安心。"),

        new("智能除醛空气净化器",339.99, "img/apps-eCommerce/30.jpg", "空净系列",5,"LONSID","优化进风口设计 配以先进的降噪系统 内置工业级球形轴承新科技操纵 高品质静音轮 有效降低转动噪音 隐藏式收纳线 收纳无忧 每一块滤网均内置RFID芯片。分体监控：分离式设计，一体式体验。语音智控：朗朗——语音控制。专效滤网定制：自由定制属于你的空气。"),
        new("智能语音车载净化器",339.99, "img/apps-eCommerce/31.jpg", "空净系列",5,"LONSID","商务典藏 科技创造 澎湃动力 双倍风压。商务典藏：商务典藏，科技创造。3.6分钟净化一遍：澎湃动力。双倍风压：双倍洁净效果。"),
        new("空气质量检测仪朗朗",339.99, "img/apps-eCommerce/32.jpg", "空净系列",5,"LONSID","完美视觉交互设计高清LED数字显示 智能语音模块 随时掌握空气状况 甲醛专业级电化学传感器 高精度温度、湿度传感器 内置独立颗粒物激光传感器。精准检测：PM2.5/甲醛/温湿度专业检测。随享智能：空气质量问问朗朗就知道，智能语音播报，不再是冰冷的机器，而是温暖的陪伴。高清LED数显：让每一次呼吸都一目了然。"),

        new("RO一体开水机（双龙头）",339.99, "img/apps-eCommerce/24.jpg", "商务机",5,"LONSID","五级净化 层层过滤 热水随取随有 使用便利 一体式设计 节省空间 加大内胆 持续供应热水。五级净化：五级净化，科学组合净化工艺。步进式加热：真正获得随取随有的热水。一体式设计：节省空间。加大内胆：配备50L热罐，可持续供应热水"),
        new("悦纯系列商用纯水机",339.99, "img/apps-eCommerce/25.jpg", "商务机",5,"LONSID","超静音制水系统、五级过滤、制水系统高回收率、高效杀菌。自动排空功能：节假日自动排空功能，保证饮水健康卫生。高回收率：制水系统高回收率，回收率大于50%。超静音制水系统：整机噪音小于50dB，强力静音，悦享健康。冷阴极UV杀菌：更安全，更卫生"),
        new("智爱系列商用直饮机",339.99, "img/apps-eCommerce/27.jpg", "商务机",5,"LONSID","超静音制水系统、五级过滤、制水系统高回收率、高效杀菌。多重安全保护：防漏电、防缺水、防干烧、防烫伤。智能显示：制水时间监控、滤芯寿命显示，IMD电控显示面板，机器运行状态一目了然。高回收率：制水系统高回收率，回收率大于50%。超静音制水系统：整机噪音小于50dB，强力静音，悦享健康。冷阴极UV杀菌：饮水更安全，更卫生"),
        new("净雅系列商用直饮机",339.99, "img/apps-eCommerce/28.jpg", "商务机",5,"LONSID","超静音制水系统、五级过滤、制水系统高回收率、高效杀菌。冷阴极UV杀菌（净雅500）：更安全，更卫生。超静音制水系统：整机噪音小于50dB，强力静音，悦享健康。高回收率：制水系统高回收率，回收率大于50%。多方监控：制水时间监控、滤芯寿命显示，IMD电控显示面板，机器运行状态一目了然。多重安全保护：防漏电、防缺水、防干烧、防烫伤。UVC-LED抑菌（净雅100）：安全卫生"),
        new("豪华商用纯水机",339.99, "img/apps-eCommerce/26.jpg", "商务机",5,"LONSID","五级净化 层层过滤、高效反渗透RO膜技术 纳米级别净化、大流量出水 适合公共场所、奢华品质 精致细节。五级净化：五级净化，科学组合净化工艺。高效反渗透膜：高效反渗透RO膜过滤技术，有效过滤有害物质。大流量出水：大流量出水，满足多人。精致细节：精致仪表盘，及时掌控机器状态。"),
        new("智爱100商用直饮机",339.99, "img/apps-eCommerce/29.jpg", "商务机",5,"LONSID","超静音制水系统、五级过滤、制水系统高回收率、高温杀菌。开水热交换系统：饮水更安全，更卫生、多重安全保护：防漏电、防缺水、防干烧、防烫伤。智能显示：制水时间监控滤芯寿命显示，IMD电控显示面板，机器运行状态一目了然。高回收率：制水系统高回收率，回收率大于50%。超静音制水系统：整机噪音小于50dB，强力静音，悦享健康。"),

        new("饮立方",339.99, "img/apps-eCommerce/20.jpg", "胶囊机",5,"LONSID","纤薄机身，小巧免安装，通电即用。智能精准,定义不同饮品最佳口感。茶胶囊可多次冲泡，家人朋友聚会齐分享。胶囊结构充氮和隔氧保鲜设计，久置冲泡亦可即刻感受香醇。高原模式，系统自动调节冲泡条件，保障不同地区使用的最佳口感。纤薄小巧：小巧免安装 随心放。智能提醒：冲泡状态一目了然。高原模式：高原模式。一茶多泡：一茶多泡 经济实惠。保持新鲜：告别变质 独立封装"),
        new("饮立方PLUS",339.99, "img/apps-eCommerce/21.jpg", "胶囊机",5,"LONSID","复合滤芯防伪设计。三机一体，定义智饮新时代。饮品智能扫码萃取，精准定义最佳口感。胶囊结构充氮和隔氧保鲜设计，即刻感受饮品香醇。茶胶囊可多次冲泡，家人朋友聚会齐分享。 保持新鲜：告别变质 独立封装。一茶多泡：一茶多泡 经济实惠。智能识别：智能识别最佳冲泡条件。"),

        new("云湃智能物联纯水机",339.99, "img/apps-eCommerce/8.jpg", "纯水机",5,"LONSID","3合1一体式专利复合滤芯、智能检测  安全防漏、超静音制水系统。深层过滤：专利复合滤芯和RO反渗透膜，持续过滤更彻底。手机监控：手机端远程便捷操作，随时随地查看机器运行状态。防漏水：一体式水路板，智能检测，降低漏水风险，放心使用。低噪音：噪音低至45分贝，家人尽享美好睡眠。自吸设计：适用于进水水压为0的使用场景，解决无压水源问题（自吸款）"),
        new("75C-2智能纯水机",339.99, "img/apps-eCommerce/9.jpg", "纯水机",5,"LONSID","进口陶氏RO膜 去除率达90%以上、五层滤芯 科学组合净化工艺、智能显示屏 触摸式按键、阻菌龙头 前置出水口全封闭、半集成水路设计 减少漏水风险。进口RO膜：进口陶氏RO膜，高节水高抗污，放心直饮。五级滤芯：五级过滤，层层过滤，健康饮水。大产水量：75G超大产水量，24小时产水280L，满足全家日常饮用水需求。阻菌龙头：阻菌龙头呵护到口，确保最后一厘米的纯净，保证你入口放心水。智能监控：直观显示滤芯寿命，触摸控制方便快捷。"),
        new("L3纯水机",339.99, "img/apps-eCommerce/10.jpg", "纯水机",5,"LONSID","高效RO膜滤芯 去除率达90%以上、五层滤芯及科学组合净化工艺、炫彩触摸式按键、独特磁吸设计机身简洁、滤芯智能冲洗 微电脑智能控制。强滤净化：好“芯”制好水，还原净水本味。反渗透过滤：纳米级精度，RO膜反渗透过滤技术，可过滤大部分有害物质，放心直饮。触摸式按键：相对于传统按钮，操作更便捷，全触摸式按键更科技。磁吸设计：人性化智能设计，自动清洗滤芯，对健康升级自动清洗技术可延长滤芯寿命。"),
        new("L2纯水机",339.99, "img/apps-eCommerce/11.jpg", "纯水机",5,"LONSID","高效RO膜滤芯 去除率达90%以上、精确寿命监控 随时掌控滤芯状况、炫彩触摸式按键、自吸式无需水压、智能化空吸保护。五级净化：好“芯”制好水，还原净水本味。反渗透过滤：纳米级精度，RO膜反渗透过滤技术，可过滤大部分有害物质，放心直饮。触摸式按键：相对于传统按钮，操作更便捷，全触摸式按键更科技。空吸保护：能够防止长时间空吸对高压泵造成的危害。自吸功能：无需水压，特殊设计的高压水泵具有自吸功能，可直接将水吸入机器净化，有效解决净水、地下水、水塔等无水压或水压不稳定的净化。"),
        new("GXRO80C 杀菌型智能纯水机",339.99, "img/apps-eCommerce/12.jpg", "纯水机",5,"LONSID","高效RO膜滤芯 去除率达90%以上、三级复合滤芯、微电脑控制 智能化保护、触摸式按键 操作更方便、独特杀菌模块 去除二次污染。高精度RO膜：RO膜的孔径非常小,只有水分子及部分有益人体的矿物离子能够通过，放心直饮，净享生活!。精“芯”呵护，体积小巧：三级滤芯，五重净化，精工打造饮水安全保障。"),
        new("L6纯水机（标准型）",339.99, "img/apps-eCommerce/13.jpg", "纯水机",5,"LONSID","采用600GRO膜 平均8S一杯水、物理阻菌 保证最后一厘米的纯净、嵌入式漏水保护监控更准确、可选择常规模式或节水模式、过流式紫外线杀菌。四级滤芯，六重过滤：优化设计重重过滤，几乎可拦截水中一切污染物。杀菌阻菌，双管齐下：UV冷阴极杀菌灯，物理阻菌龙头，让你喝上放心水。超大流量，集成水路：8S一杯水节省时间，一体式水路减少漏水风险和噪音。节省空间，维护便捷：无桶设计释放厨房空间，无需工具一提一拉秒速换芯。智能屏显，自由选择：屏幕实时选择工作状态，出现漏水强制停机，常规模式节水模式任你选择。"),
        new("S1纯水机",339.99, "img/apps-eCommerce/14.jpg", "纯水机",5,"LONSID","4合1全新高集成滤芯设计 高端智能按键龙头 1:1超低废水比 安全防伪二维码 高性能紫外线杀菌技术 。机身纤巧：安装不受限。智能双屏：龙头、机身双屏幕显示，水质、滤芯寿命一目了然。集成滤芯：四合一滤芯，省心省力省空间，高抗污染RO膜，滤芯性能更卓越。防伪验证：扫一扫可辨真伪，更换滤芯时，只有二维码被识别后方可更换。"),

        new("R2中央软水机",339.99, "img/apps-eCommerce/3.jpg", "中央处理设备",5,"LONSID","食品卫生安全材料、核心部件安全保障、智慧流量延滞型再生模式、比同类产品省33%再生剂和65%的水、智能自动循环运行。"),
        new("Q3全自动智能冲洗前置过滤器",339.99, "img/apps-eCommerce/2.jpg", "中央处理设备",5,"LONSID","环保无铅 国标62铜材质、食品级高分子防爆瓶体、滤瓶刮洗 免拆滤芯、万向接头 安装不受限、可拆卸透明窗体。"),
        new("J2中央净水机",339.99, "img/apps-eCommerce/5.jpg", "中央处理设备",5,"LONSID","高性能高效去除水中余氯、核心部件安全保障、智慧流量延滞型再生模式、比同类产品省33%再生剂和65%的水、智能自动循环运行。"),
        new("R3中央软水机",339.99, "img/apps-eCommerce/4.jpg", "中央处理设备",5,"LONSID","大流量匹配大用水量、食品级高容量离子交换树脂、省盐省水、大集成智能控制系统、旁通阀设计。"),

        new(Guid.Parse("2d1e3550-5135-50c8-836f-f988bfef91c8"),"Apple Watch Series 5",339.99,"/img/apps-eCommerce/1.png","Cell Phones",5,"Apple","On Retina display that never sleeps, so it’s easy to see the time and other important information, without raising or tapping the display. New location features, from a built-in compass to current elevation, help users better navigate their day, while international emergency calling1 allows customers to call emergency services directly from Apple Watch in over 150 countries, even without iPhone nearby. Apple Watch Series 5 is available in a wider range of materials, including aluminium, stainless steel, ceramic and an all-new titanium."),
        new("Apple iPhone 11 (65GB, Black)",669.99,"/img/apps-eCommerce/2.png","Cell Phones",5,"Apple","The Apple iPhone 11 is a great smartphone, which was loaded with a lot of quality features. It comes with a waterproof and dustproof body which is the key attraction of the device. The excellent set of cameras offer excellent images as well as capable of recording crisp videos. However, expandable storage and a fingerprint scanner would have made it a perfect option to go for around this price range."),
        new("Apple iMac 27-inch",999.99,"/img/apps-eCommerce/3.png","Computers & Tablets",5,"Apple","The all-in-one for all. If you can dream it, you can do it on iMac. It’s beautifully & incredibly intuitive and packed with tools that let you take any idea to the next level. And the new 27-inch model elevates the experience in way, with faster processors and graphics, expanded memory and storage, enhanced audio and video capabilities, and an even more stunning Retina 5K display. It’s the desktop that does it all — better and faster than ever."),
        new("OneOdio A71 Wired Headphones",59.99,"/img/apps-eCommerce/5.png","Audio",3,"OneOdio","Omnidirectional detachable boom mic upgrades the headphones into a professional headset for gaming, business, podcasting and taking calls on the go. Better pick up your voice. Control most electric devices through voice activation, or schedule a ride with Uber and order a pizza. OneOdio A71 Wired Headphones voice-controlled device turns any home into a smart device on a smartphone or tablet."),
        new("Apple - MacBook Air® (Latest Model) - 13.3 Display - Silver",999.99,"/img/apps-eCommerce/5.png","Computers & Tablets",5,"Apple","MacBook Air is a thin, lightweight laptop from Apple. MacBook Air features up to 8GB of memory, a fifth-generation Intel Core processor, Thunderbolt 2, great built-in apps, and all-day battery life.1 Its thin, light, and durable enough to take everywhere you go-and powerful enough to do everything once you get there, better."),
        new("Switch Pro Controller",529.99,"/img/apps-eCommerce/6.png","Video Games",3,"Sharp","The Nintendo Switch Pro Controller is one of the priciest baseline controllers in the current console generation, but it's also sturdy, feels good to play with, has an excellent direction pad, and features impressive motion sensors and vibration systems. On top of all of that, it uses Bluetooth, so you don't need an adapter to use it with your PC."),
        new("Google - Google Home - White/Slate fabric",129.29,"/img/apps-eCommerce/7.png","Audio",5,"Google","Simplify your everyday life with the Google Home, a voice-activated speaker powered by the Google Assistant. Use voice commands to enjoy music, get answers from Google and manage everyday tasks. Google Home is compatible with Android and iOS operating systems, and can control compatible smart devices such as Chromecast or Nest."),
        new("Sony 5K Ultra HD LED TV",7999.99,"/img/apps-eCommerce/8.png","Computers & Tablets",5,"Apple","Sony 5K Ultra HD LED TV has 5K HDR Support. The TV provides clear visuals and provides distinct sound quality and an immersive experience. This TV has Yes HDMI ports & Yes USB ports. Connectivity options included are HDMI. You can connect various gadgets such as your laptop using the HDMI port. The TV comes with a 1 Year warranty."),
        new("OnePlus 7 Pro",15.99,"/img/apps-eCommerce/9.png","Cell Phones",5,"Philips","The OnePlus 7 Pro features a brand new design, with a glass back and front and curved sides. The phone feels very premium but’s it’s also very heavy. The Nebula Blue variant looks slick but it’s quite slippery, which makes single-handed use a real challenge. It has a massive 6.67-inch ‘Fluid AMOLED’ display with a QHD+ resolution, 90Hz refresh rate and support for HDR 10+ content. The display produces vivid colours, deep blacks and has good viewing angles."),
        new("Logitech K380 Wireless Keyboard",81.99,"/img/apps-eCommerce/10.png","Office & School Supplies",5,"Logitech","Logitech K380 Bluetooth Wireless Keyboard gives you the comfort and convenience of desktop typing on your smartphone, and tablet. It is a wireless keyboard that connects to all Bluetooth wireless devices that support external keyboards. Take this compact, lightweight, Bluetooth keyboard anywhere in your home. Type wherever you like, on any compatible computer, phone or tablet."),
        new("Nike Air Max",81.99,"/img/apps-eCommerce/11.png","Health, Fitness & Beauty",5,"Nike","With a bold application of colorblocking inspired by modern art styles, the Nike Air Max 270 React sneaker is constructed with layers of lightweight material to achieve its artful look and comfortable feel."),
        new("New Apple iPad Pro",799.99,"/img/apps-eCommerce/12.png","Computers & Tablets",3,"Apple","Up to 10 hours of surﬁng the web on Wi‑Fi, watching video, or listening to music. Up to 9 hours of surﬁng the web using cellular data network, Compatible with Smart Keyboard Folio and Bluetooth keyboards."),
        new("Vankyo leisure 3 mini projector",99.99,"/img/apps-eCommerce/13.png","TV & Home Theater",2,"Vankyo Store","SUPERIOR VIEWING EXPERIENCE: Supporting 1920x1080 resolution, VANKYO Leisure 3 projector is powered by MStar Advanced Color Engine, which is ideal for home entertainment. 2020 upgraded LED lighting provides a superior viewing experience for you."),
        new("Wireless Charger 5W Max",10.83,"/img/apps-eCommerce/15.png","Appliances",3,"3M","Charge with case: transmits charging power directly through protective cases. Rubber/plastic/TPU cases under 5 mm thickness . Do not use any magnetic and metal attachments or cards, or it will prevent charging."),
        new("Laptop Bag",29.99,"/img/apps-eCommerce/15.png","Office & School Supplies",5,"TAS","TSA FRIENDLY- A separate DIGI SMART compartment can hold 15.6 inch Laptop as well as 15 inch, 15 inch Macbook, 12.9 inch iPad, and tech accessories like charger for quick TSA checkpoint when traveling."),
        new("Adidas Mens Tech Response Shoes",55.59,"/img/apps-eCommerce/16.png","Health, Fitness & Beauty",5,"Adidas","Comfort + performance. Designed with materials that are durable, lightweight and extremely comfortable. Core performance delivers the perfect mix of fit, style and all-around performance."),
        new("Handbags for Women Large Designer bag",39.99,"/img/apps-eCommerce/17.png","Office & School Supplies",5,"Hobo","Classic Hobo Purse: Top zipper closure, with 2 side zipper pockets design and elegant tassels decoration, fashionable and practical handbags for women, perfect for shopping, dating, travel and business."),
        new("Oculus Quest All-in-one VR",655,"/img/apps-eCommerce/18.png","TV & Home Theater",1,"Oculus","All-in-one VR: No PC. No wires. No limits. Oculus quest is an all-in-one gaming system built for virtual reality. Now you can play almost anywhere with just a VR headset and controllers. Oculus touch controllers: arm yourself with the award-winning Oculus touch controllers. Your slashes, throws and grab appear in VR with intuitive, realistic Precision, transporting your hands and gestures right into the game"),
        new("Giotto 32oz Leakproof BPA Free Drinking Water",16.99,"/img/apps-eCommerce/19.png","Health, Fitness & Beauty",5,"3m","With unique inspirational quote and time markers on it,this water bottle is great for measuring your daily intake of water,reminding you stay hydrated and drink enough water throughout the day.A must have for any fitness goals including weight loss,appetite control and overall health."),
        new("PlayStation 5 Console",339.99,"/img/apps-eCommerce/20.png","Video Games",5,"Sony","All the greatest, games, TV, music and more. Connect with your friends to broadcast and celebrate your epic moments at the press of the Share button to Twitch, YouTube, Facebook and Twitter."),
        new("Bugani M90 Portable Bluetooth Speaker",56,"/img/apps-eCommerce/20.png","Audio",3,"Bugani","Bluetooth Speakers-The M90 Bluetooth speaker uses the latest Bluetooth 5.0 technology and the latest Bluetooth ATS chip, Connecting over Bluetooth in seconds to iPhone, iPad, Smart-phones, Tablets, Windows, and other Bluetooth devices."),
        new("Tile Pro - High Performance Bluetooth Tracker",29.99,"/img/apps-eCommerce/22.png","Office & School Supplies",5,"Tile","FIND KEYS, BAGS & MORE -- Pro is our high-performance finder ideal for keys, backpacks, luggage or any other items you want to Favorite track of. The easy-to-use finder and free app work with iOS and Android."),
        new("Toshiba Canvio Advance 2TB Portable External Hard Drive",69.99,"/img/apps-eCommerce/23.png","Computers & Tablets",2,"Toshiba","Up to 3TB of storage capacity to store your growing files and content."),
        new("Ronyes Unisex College Bag Bookbags for Women",23.99,"/img/apps-eCommerce/25.png","Office & School Supplies",2,"Ronyes","Made of high quality water-resistant material; padded and adjustable shoulder straps; external USB with built-in charging cable offers a convenient charging"),
        new("Willful Smart Watch for Men Women 2020",29.99,"/img/apps-eCommerce/25.png","Cell Phones",5,"Willful","Are you looking for a smart watch, which can not only easily Favorite tracking of your steps, calories, heart rate and sleep quality, but also Favorite you informed of incoming calls."),
        new("Rectangular Polarized, Bluetooth Audio Sunglasses",259,"/img/apps-eCommerce/26.png","Health, Fitness & Beauty",5,"Bose","Redesigned for luxury — Thoughtfully refined and strikingly elegant, the latest Bose sunglasses blend enhanced features and designs for an elevated way to listen."),
        new("VicTsing Wireless Mouse",10.99,"/img/apps-eCommerce/27.png","Computers & Tablets",3,"VicTsing","After thousands of samples of palm data, we designed this ergonomic mouse. The laptop mouse has a streamlined arc and thumb rest to help reduce the stress caused by prolonged use of the laptop mouse."),
    };

    public static List<GoodsDto> GetGoodsList() => _datas;

    public static List<GoodsDto> GetRelatedGoodsList() => GetGoodsList().GetRange(0, 10);

    public static List<MultiRangeDto> GetMultiRangeList() => new ()
    {
        new(RangeType.All,"All",0),
        new(RangeType.LessEqual,"<= $10",10),
        new(RangeType.Range,"$10 - $100",10,100),
        new(RangeType.Range,"$100 - $500",100,500),
        new(RangeType.MoreEqual,">=$500",500),
    };

    public static List<string> GetCategortyList() => new()
    {
        "饮水机",
        "纯水机",
        "商务机",
        "胶囊机",
        "空净系列",
        "中央处理设备",
        "Cell Phones",
    };

    public static List<string> GetBrandList() => new()
    {
        "LONSID",
        "Apple"
    };
}

