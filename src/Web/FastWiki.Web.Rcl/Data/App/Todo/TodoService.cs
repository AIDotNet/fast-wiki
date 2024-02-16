namespace Masa.Blazor.Pro.Data.App.Todo;

public class TodoService
{
    public static List<TodoDto> GetList() => new()
    {
        new TodoDto(1, false, false, false, false, "汉皇重色思倾国，御宇多年求不得。", "紫萱", 0, new DateOnly(2021, 9, 15), new List<string> { "Low", }, "1、最灵繁的人也看不见自己的背脊。——非洲"),
        new TodoDto(2, false, false, false, false, "杨家有女初长成，养在深闺人未识。", "若芹", 2, new DateOnly(2021, 9, 16), new List<string> { "Low" }, "2、最困难的事情就是认识自己。——希腊"),
        new TodoDto(3, true, false, true, false, "天生丽质难自弃，一朝选在君王侧。", "思菱", 5, new DateOnly(2021, 9, 17), new List<string> { "Low" }, "3、有勇气承担命运这才是英雄好汉。——黑塞"),
        new TodoDto(4, true, false, true, false, "回眸一笑百媚生，六宫粉黛无颜色。", "向秋", 3, new DateOnly(2021, 9, 18), new List<string> { "Low" }, "4、与肝胆人共事，无字句处读书。——周恩来"),
        new TodoDto(5, true, true, true, false, "春寒赐浴华清池，温泉水滑洗凝脂。", "雨珍", 4, new DateOnly(2021, 9, 19), new List<string> { "Low" }, "5、阅读使人充实，会谈使人敏捷，写作使人精确。——培根"),
        new TodoDto(6, false, true, false, false, "侍儿扶起娇无力，始是新承恩泽时。", "海瑶", 1, new DateOnly(2021, 9, 20), new List<string> { "High" }, "6、最大的骄傲于最大的自卑都表示心灵的最软弱无力。——斯宾诺莎"),
        new TodoDto(7, true, true, true, false, "云鬓花颜金步摇，芙蓉帐暖度春宵。", "乐萱", 2, new DateOnly(2021, 9, 21), new List<string> { "High" }, "7、自知之明是最难得的知识。——西班牙"),
        new TodoDto(8, true, true, false, false, "春宵苦短日高起，从此君王不早朝。", "紫萱", 5, new DateOnly(2021, 9, 22), new List<string> { "High" }, "8、勇气通往天堂，怯懦通往地狱。——塞内加"),
        new TodoDto(9, true, false, false, false, "承欢侍宴无闲暇，春从春游夜专夜。", "若芹", 3, new DateOnly(2021, 9, 23), new List<string> { "High" }, "9、有时候读书是一种巧妙地避开思考的方法。——赫尔普斯"),
        new TodoDto(10, true, false, false, false, "后宫佳丽三千人，三千宠爱在一身。", "思菱", 1, new DateOnly(2021, 9, 24), new List<string> { "High" }, "10、阅读一切好书如同和过去最杰出的人谈话。——笛卡儿"),
        new TodoDto(11, true, false, true, false, "金屋妆成娇侍夜，玉楼宴罢醉和春。", "向秋", 4, new DateOnly(2021, 9, 25), new List<string> { "High" }, "11、越是没有本领的就越加自命不凡。——邓拓"),
        new TodoDto(12, true, false, false, false, "姊妹弟兄皆列土，可怜光彩生门户。", "雨珍", 0, new DateOnly(2021, 9, 26), new List<string> { "High" }, "12、越是无能的人，越喜欢挑剔别人的错儿。——爱尔兰"),
        new TodoDto(13, true, true, false, false, "遂令天下父母心，不重生男重生女。", "海瑶", 5, new DateOnly(2021, 9, 27), new List<string> { "Medium" }, "13、知人者智，自知者明。胜人者有力，自胜者强。——老子"),
        new TodoDto(14, true, true, false, false, "骊宫高处入青云，仙乐风飘处处闻。", "乐萱", 4, new DateOnly(2021, 9, 28), new List<string> { "Medium" }, "14、意志坚强的人能把世界放在手中像泥块一样任意揉捏。——歌德"),
        new TodoDto(15, true, true, false, false, "缓歌慢舞凝丝竹，尽日君王看不足。", "紫萱", 1, new DateOnly(2021, 9, 29), new List<string> { "Medium" }, "15、最具挑战性的挑战莫过于提升自我。——迈克尔·F·斯特利"),
        new TodoDto(16, false, false, false, false, "渔阳鼙鼓动地来，惊破霓裳羽衣曲。", "若芹", 4, new DateOnly(2021, 9, 30), new List<string> { "Medium" }, "16、业余生活要有意义，不要越轨。——华盛顿"),
        new TodoDto(17, false, true, false, false, "九重城阙烟尘生，千乘万骑西南行。", "思菱", 4, new DateOnly(2021, 10, 1), new List<string> { "Medium" }, "17、一个人即使已登上顶峰，也仍要自强不息。——罗素·贝克"),
        new TodoDto(18, false, false, false, false, "翠华摇摇行复止，西出都门百余里。", "向秋", 4, new DateOnly(2021, 10, 2), new List<string> { "Medium" }, "18、最大的挑战和突破在于用人，而用人最大的突破在于信任人。——马云"),
        new TodoDto(19, false, true, false, false, "六军不发无奈何，宛转蛾眉马前死。", "雨珍", 0, new DateOnly(2021, 10, 3), new List<string> { "Medium" }, "19、自己活着，就是为了使别人过得更美好。——雷锋"),
        new TodoDto(20, false, false, false, false, "花钿委地无人收，翠翘金雀玉搔头。", "海瑶", 0, new DateOnly(2021, 10, 4), new List<string> { "Team" }, "20、要掌握书，莫被书掌握；要为生而读，莫为读而生。——布尔沃"),
        new TodoDto(21, false, false, false, false, "君王掩面救不得，回看血泪相和流。", "乐萱", 5, new DateOnly(2021, 10, 5), new List<string> { "Team" }, "21、要知道对好事的称颂过于夸大，也会招来人们的反感轻蔑和嫉妒。——培根"),
        new TodoDto(22, false, false, false, false, "黄埃散漫风萧索，云栈萦纡登剑阁。", "紫萱", 4, new DateOnly(2021, 10, 6), new List<string> { "Team" }, "22、业精于勤，荒于嬉；行成于思，毁于随。——韩愈"),
        new TodoDto(23, true, true, true, false, "峨嵋山下少人行，旌旗无光日色薄。", "若芹", 4, new DateOnly(2021, 10, 7), new List<string> { "Team" }, "23、一切节省，归根到底都归结为时间的节省。——马克思"),
        new TodoDto(24, false, false, false, false, "蜀江水碧蜀山青，圣主朝朝暮暮情。", "思菱", 4, new DateOnly(2021, 10, 8), new List<string> { "Team" }, "24、意志命运往往背道而驰，决心到最后会全部推倒。——莎士比亚"),
        new TodoDto(25, true, false, true, false, "行宫见月伤心色，夜雨闻铃肠断声。", "向秋", 1, new DateOnly(2021, 10, 9), new List<string> { "Team" }, "25、学习是劳动，是充满思想的劳动。——乌申斯基"),
        new TodoDto(26, false, true, false, true, "天旋地转回龙驭，到此踌躇不能去。", "雨珍", 1, new DateOnly(2021, 10, 10), new List<string> { "Team" }, "26、要使整个人生都过得舒适、愉快，这是不可能的，因为人类必须具备一种能应付逆境的态度。——卢梭"),
        new TodoDto(27, true, false, false, false, "马嵬坡下泥土中，不见玉颜空死处。", "海瑶", 2, new DateOnly(2021, 10, 11), new List<string> { "Team" }, "27、只有把抱怨环境的心情，化为上进的力量，才是成功的保证。——罗曼·罗兰"),
        new TodoDto(28, true, false, true, true, "君臣相顾尽沾衣，东望都门信马归。", "乐萱", 2, new DateOnly(2021, 10, 12), new List<string> { "Low" }, "28、知之者不如好之者，好之者不如乐之者。——孔子"),
        new TodoDto(29, true, true, false, false, "归来池苑皆依旧，太液芙蓉未央柳。", "紫萱", 1, new DateOnly(2021, 10, 13), new List<string> { "Low" }, "29、勇猛、大胆和坚定的决心能够抵得上武器的精良。——达·芬奇"),
        new TodoDto(30, true, false, false, false, "芙蓉如面柳如眉，对此如何不泪垂。", "若芹", 5, new DateOnly(2021, 10, 14), new List<string> { "Low" }, "30、意志是一个强壮的盲人，倚靠在明眼的跛子肩上。——叔本华"),
        new TodoDto(31, true, false, false, false, "春风桃李花开夜，秋雨梧桐叶落时。", "思菱", 1, new DateOnly(2021, 10, 15), new List<string> { "Low" }, "31、只有永远躺在泥坑里的人，才不会再掉进坑里。——黑格尔"),
        new TodoDto(32, true, false, true, true, "西宫南苑多秋草，落叶满阶红不扫。", "向秋", 3, new DateOnly(2021, 10, 16), new List<string> { "Low" }, "32、希望的灯一旦熄灭，生活刹那间变成了一片黑暗。——普列姆昌德"),
        new TodoDto(33, true, false, false, false, "梨园弟子白发新，椒房阿监青娥老。", "雨珍", 5, new DateOnly(2021, 10, 17), new List<string> { "Low" }, "33、希望是人生的乳母。——科策布"),
        new TodoDto(34, true, true, true, false, "夕殿萤飞思悄然，孤灯挑尽未成眠。", "海瑶", 1, new DateOnly(2021, 10, 18), new List<string> { "Low" }, "34、形成天才的决定因素应该是勤奋。——郭沫若"),
        new TodoDto(35, false, false, false, false, "迟迟钟鼓初长夜，耿耿星河欲曙天。", "乐萱", 1, new DateOnly(2021, 10, 19), new List<string> { "High" }, "35、学到很多东西的诀窍，就是一下子不要学很多。——洛克"),
        new TodoDto(36, false, true, false, true, "鸳鸯瓦冷霜华重，翡翠衾寒谁与共。", "紫萱", 1, new DateOnly(2021, 10, 20), new List<string> { "High" }, "36、自己的鞋子，自己知道紧在哪里。——西班牙"),
        new TodoDto(37, false, false, false, false, "悠悠生死别经年，魂魄不曾来入梦。", "若芹", 2, new DateOnly(2021, 10, 21), new List<string> { "High" }, "37、我们唯一不会改正的缺点是软弱。——拉罗什福科"),
        new TodoDto(38, false, false, false, true, "临邛道士鸿都客，能以精诚致魂魄。", "思菱", 0, new DateOnly(2021, 10, 22), new List<string> { "High" }, "38、我这个人走得很慢，但是我从不后退。——亚伯拉罕·林肯"),
        new TodoDto(39, true, false, true, false, "为感君王辗转思，遂教方士殷勤觅。", "向秋", 1, new DateOnly(2021, 10, 23), new List<string> { "High" }, "39、勿问成功的秘诀为何，且尽全力做你应该做的事吧。——美华纳"),
        new TodoDto(40, false, false, false, false, "排空驭气奔如电，升天入地求之遍。", "雨珍", 4, new DateOnly(2021, 10, 24), new List<string> { "High" }, "40、学而不思则罔，思而不学则殆。——孔子"),
        new TodoDto(41, true, true, true, true, "上穷碧落下黄泉，两处茫茫皆不见。", "海瑶", 1, new DateOnly(2021, 10, 25), new List<string> { "High" }, "41、学问是异常珍贵的东西，从任何源泉吸收都不可耻。——阿卜·日·法拉兹"),
        new TodoDto(42, false, false, false, false, "忽闻海上有仙山，山在虚无缥渺间。", "乐萱", 3, new DateOnly(2021, 10, 26), new List<string> { "High" }, "42、只有在人群中间，才能认识自己。——德国"),
        new TodoDto(43, false, true, false, false, "楼阁玲珑五云起，其中绰约多仙子。", "紫萱", 5, new DateOnly(2021, 10, 27), new List<string> { "High" }, "43、重复别人所说的话，只需要教育；而要挑战别人所说的话，则需要头脑。——玛丽·佩蒂博恩·普尔"),
        new TodoDto(44, false, false, false, true, "中有一人字太真，雪肤花貌参差是。", "若芹", 1, new DateOnly(2021, 10, 28), new List<string> { "High" }, "44、卓越的人一大优点是：在不利与艰难的遭遇里百折不饶。——贝多芬"),
        new TodoDto(45, false, false, false, false, "金阙西厢叩玉扃，转教小玉报双成。", "思菱", 2, new DateOnly(2021, 10, 29), new List<string> { "High" }, "45、自己的饭量自己知道。——苏联"),
        new TodoDto(46, true, false, true, false, "闻道汉家天子使，九华帐里梦魂惊。", "向秋", 2, new DateOnly(2021, 10, 30), new List<string> { "High" }, "46、我们若已接受最坏的，就再没有什么损失。——卡耐基"),
        new TodoDto(47, false, false, false, false, "揽衣推枕起徘徊，珠箔银屏迤逦开。", "雨珍", 5, new DateOnly(2021, 11, 1), new List<string> { "High" }, "47、书到用时方恨少、事非经过不知难。——陆游"),
        new TodoDto(48, false, false, false, false, "云鬓半偏新睡觉，花冠不整下堂来。", "海瑶", 1, new DateOnly(2021, 11, 2), new List<string> { "Update" }, "48、书籍把我们引入最美好的社会，使我们认识各个时代的伟大智者。——史美尔斯"),
        new TodoDto(49, true, false, true, false, "风吹仙袂飘飖举，犹似霓裳羽衣舞。", "乐萱", 1, new DateOnly(2021, 11, 3), new List<string> { "Update" }, "49、熟读唐诗三百首，不会作诗也会吟。——孙洙"),
        new TodoDto(50, true, true, true, false, "玉容寂寞泪阑干，梨花一枝春带雨。", "紫萱", 4, new DateOnly(2021, 11, 4), new List<string> { "Update" }, "50、谁和我一样用功，谁就会和我一样成功。——莫扎特"),
        new TodoDto(51, true, true, true, false, "含情凝睇谢君王，一别音容两渺茫。", "若芹", 4, new DateOnly(2021, 11, 5), new List<string> { "Update" }, "51、天下之事常成于困约，而败于奢靡。——陆游"),
        new TodoDto(52, true, true, false, false, "昭阳殿里恩爱绝，蓬莱宫中日月长。", "思菱", 5, new DateOnly(2021, 11, 6), new List<string> { "Update" }, "52、生命不等于是呼吸，生命是活动。——卢梭"),
        new TodoDto(53, true, true, false, false, "回头下望人寰处，不见长安见尘雾。", "向秋", 1, new DateOnly(2021, 11, 7), new List<string> { "Update" }, "53、伟大的事业，需要决心，能力，组织和责任感。　——易卜生"),
        new TodoDto(54, true, false, false, false, "惟将旧物表深情，钿合金钗寄将去。", "雨珍", 1, new DateOnly(2021, 11, 8), new List<string> { "Update" }, "54、唯书籍不朽。——乔特"),
        new TodoDto(55, true, true, true, false, "钗留一股合一扇，钗擘黄金合分钿。", "海瑶", 3, new DateOnly(2021, 11, 9), new List<string> { "Update" }, "55、为中华之崛起而读书。——周恩来"),
        new TodoDto(56, false, false, false, false, "但令心似金钿坚，天上人间会相见。", "乐萱", 3, new DateOnly(2021, 11, 10), new List<string> { "Update" }, "56、书不仅是生活，而且是现在、过去和未来文化生活的源泉。——库法耶夫"),
        new TodoDto(57, true, false, false, false, "临别殷勤重寄词，词中有誓两心知。", "紫萱", 1, new DateOnly(2021, 11, 11), new List<string> { "Update" }, "57、生命不可能有两次，但许多人连一次也不善于度过。——吕凯特"),
        new TodoDto(58, false, false, false, false, "七月七日长生殿，夜半无人私语时。", "若芹", 5, new DateOnly(2021, 11, 12), new List<string> { "Update" }, "58、问渠哪得清如许，为有源头活水来。——朱熹"),
        new TodoDto(59, true, false, false, false, "在天愿作比翼鸟，在地愿为连理枝。", "思菱", 5, new DateOnly(2021, 11, 13), new List<string> { "Update" }, "59、我的努力求学没有得到别的好处，只不过是愈来愈发觉自己的无知。——笛卡儿"),
        new TodoDto(60, true, false, true, false, "天长地久有时尽，此恨绵绵无绝期。", "向秋", 1, new DateOnly(2021, 11, 14), new List<string> { "Update" }, "60、生活的道路一旦选定，就要勇敢地走到底，决不回头。——左拉"),
    };

    public static List<SelectData> GetAssigneeList() => new()
    {
        new SelectData() { Label = "紫萱", Value = "紫萱" },
        new SelectData() { Label = "若芹", Value = "若芹" },
        new SelectData() { Label = "思菱", Value = "思菱" },
        new SelectData() { Label = "向秋", Value = "向秋" },
        new SelectData() { Label = "雨珍", Value = "雨珍" },
        new SelectData() { Label = "海瑶", Value = "海瑶" },
        new SelectData() { Label = "乐萱", Value = "乐萱" },
    };

    public static List<SelectData> GetTagList() => new()
    {
        new SelectData() { Label = "Team", Value = "Team" },
        new SelectData() { Label = "Low", Value = "Low" },
        new SelectData() { Label = "Medium", Value = "Medium" },
        new SelectData() { Label = "High", Value = "High" },
        new SelectData() { Label = "Update", Value = "Update" }
    };

    public static Dictionary<string, string> GetTagColorMap() => new()
    {
        { "Team", "pry" },
        { "Low", "sample-green" },
        { "Medium", "remind" },
        { "High", "error" },
        { "Update", "info" },
    };

    public static string[] GetAvatars() => new string[]
    {
        "/img/avatar/1.svg",
        "/img/avatar/8.svg",
        "/img/avatar/12.svg",
        "/img/avatar/10.svg",
        "/img/avatar/11.svg",
        "/img/avatar/9.svg"
    };
}
