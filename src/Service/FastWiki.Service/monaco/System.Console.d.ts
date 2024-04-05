
namespace System {
   /** Specifies constants that define foreground and background colors for the console. */
   enum ConsoleColor {
      /** The color black. */
      Black = 0,

      /** The color dark blue. */
      DarkBlue = 1,

      /** The color dark green. */
      DarkGreen = 2,

      /** The color dark cyan (dark blue-green). */
      DarkCyan = 3,

      /** The color dark red. */
      DarkRed = 4,

      /** The color dark magenta (dark purplish-red). */
      DarkMagenta = 5,

      /** The color dark yellow (ochre). */
      DarkYellow = 6,

      /** The color gray. */
      Gray = 7,

      /** The color dark gray. */
      DarkGray = 8,

      /** The color blue. */
      Blue = 9,

      /** The color green. */
      Green = 10,

      /** The color cyan (blue-green). */
      Cyan = 11,

      /** The color red. */
      Red = 12,

      /** The color magenta (purplish-red). */
      Magenta = 13,

      /** The color yellow. */
      Yellow = 14,

      /** The color white. */
      White = 15,
   }
}

namespace System {
   /** Specifies the standard keys on a console. */
   enum ConsoleKey {
      /** The BACKSPACE key. */
      Backspace = 8,

      /** The TAB key. */
      Tab = 9,

      /** The CLEAR key. */
      Clear = 12,

      /** The ENTER key. */
      Enter = 13,

      /** The PAUSE key. */
      Pause = 19,

      /** The ESC (ESCAPE) key. */
      Escape = 27,

      /** The SPACEBAR key. */
      Spacebar = 32,

      /** The PAGE UP key. */
      PageUp = 33,

      /** The PAGE DOWN key. */
      PageDown = 34,

      /** The END key. */
      End = 35,

      /** The HOME key. */
      Home = 36,

      /** The LEFT ARROW key. */
      LeftArrow = 37,

      /** The UP ARROW key. */
      UpArrow = 38,

      /** The RIGHT ARROW key. */
      RightArrow = 39,

      /** The DOWN ARROW key. */
      DownArrow = 40,

      /** The SELECT key. */
      Select = 41,

      /** The PRINT key. */
      Print = 42,

      /** The EXECUTE key. */
      Execute = 43,

      /** The PRINT SCREEN key. */
      PrintScreen = 44,

      /** The INS (INSERT) key. */
      Insert = 45,

      /** The DEL (DELETE) key. */
      Delete = 46,

      /** The HELP key. */
      Help = 47,

      /** The 0 key. */
      D0 = 48,

      /** The 1 key. */
      D1 = 49,

      /** The 2 key. */
      D2 = 50,

      /** The 3 key. */
      D3 = 51,

      /** The 4 key. */
      D4 = 52,

      /** The 5 key. */
      D5 = 53,

      /** The 6 key. */
      D6 = 54,

      /** The 7 key. */
      D7 = 55,

      /** The 8 key. */
      D8 = 56,

      /** The 9 key. */
      D9 = 57,

      /** The A key. */
      A = 65,

      /** The B key. */
      B = 66,

      /** The C key. */
      C = 67,

      /** The D key. */
      D = 68,

      /** The E key. */
      E = 69,

      /** The F key. */
      F = 70,

      /** The G key. */
      G = 71,

      /** The H key. */
      H = 72,

      /** The I key. */
      I = 73,

      /** The J key. */
      J = 74,

      /** The K key. */
      K = 75,

      /** The L key. */
      L = 76,

      /** The M key. */
      M = 77,

      /** The N key. */
      N = 78,

      /** The O key. */
      O = 79,

      /** The P key. */
      P = 80,

      /** The Q key. */
      Q = 81,

      /** The R key. */
      R = 82,

      /** The S key. */
      S = 83,

      /** The T key. */
      T = 84,

      /** The U key. */
      U = 85,

      /** The V key. */
      V = 86,

      /** The W key. */
      W = 87,

      /** The X key. */
      X = 88,

      /** The Y key. */
      Y = 89,

      /** The Z key. */
      Z = 90,

      /** The left Windows logo key (Microsoft Natural Keyboard). */
      LeftWindows = 91,

      /** The right Windows logo key (Microsoft Natural Keyboard). */
      RightWindows = 92,

      /** The Application key (Microsoft Natural Keyboard). */
      Applications = 93,

      /** The Computer Sleep key. */
      Sleep = 95,

      /** The 0 key on the numeric keypad. */
      NumPad0 = 96,

      /** The 1 key on the numeric keypad. */
      NumPad1 = 97,

      /** The 2 key on the numeric keypad. */
      NumPad2 = 98,

      /** The 3 key on the numeric keypad. */
      NumPad3 = 99,

      /** The 4 key on the numeric keypad. */
      NumPad4 = 100,

      /** The 5 key on the numeric keypad. */
      NumPad5 = 101,

      /** The 6 key on the numeric keypad. */
      NumPad6 = 102,

      /** The 7 key on the numeric keypad. */
      NumPad7 = 103,

      /** The 8 key on the numeric keypad. */
      NumPad8 = 104,

      /** The 9 key on the numeric keypad. */
      NumPad9 = 105,

      /** The Multiply key (the multiplication key on the numeric keypad). */
      Multiply = 106,

      /** The Add key (the addition key on the numeric keypad). */
      Add = 107,

      /** The Separator key. */
      Separator = 108,

      /** The Subtract key (the subtraction key on the numeric keypad). */
      Subtract = 109,

      /** The Decimal key (the decimal key on the numeric keypad). */
      Decimal = 110,

      /** The Divide key (the division key on the numeric keypad). */
      Divide = 111,

      /** The F1 key. */
      F1 = 112,

      /** The F2 key. */
      F2 = 113,

      /** The F3 key. */
      F3 = 114,

      /** The F4 key. */
      F4 = 115,

      /** The F5 key. */
      F5 = 116,

      /** The F6 key. */
      F6 = 117,

      /** The F7 key. */
      F7 = 118,

      /** The F8 key. */
      F8 = 119,

      /** The F9 key. */
      F9 = 120,

      /** The F10 key. */
      F10 = 121,

      /** The F11 key. */
      F11 = 122,

      /** The F12 key. */
      F12 = 123,

      /** The F13 key. */
      F13 = 124,

      /** The F14 key. */
      F14 = 125,

      /** The F15 key. */
      F15 = 126,

      /** The F16 key. */
      F16 = 127,

      /** The F17 key. */
      F17 = 128,

      /** The F18 key. */
      F18 = 129,

      /** The F19 key. */
      F19 = 130,

      /** The F20 key. */
      F20 = 131,

      /** The F21 key. */
      F21 = 132,

      /** The F22 key. */
      F22 = 133,

      /** The F23 key. */
      F23 = 134,

      /** The F24 key. */
      F24 = 135,

      /** The Browser Back key. */
      BrowserBack = 166,

      /** The Browser Forward key. */
      BrowserForward = 167,

      /** The Browser Refresh key. */
      BrowserRefresh = 168,

      /** The Browser Stop key. */
      BrowserStop = 169,

      /** The Browser Search key. */
      BrowserSearch = 170,

      /** The Browser Favorites key. */
      BrowserFavorites = 171,

      /** The Browser Home key. */
      BrowserHome = 172,

      /** The Volume Mute key (Microsoft Natural Keyboard). */
      VolumeMute = 173,

      /** The Volume Down key (Microsoft Natural Keyboard). */
      VolumeDown = 174,

      /** The Volume Up key (Microsoft Natural Keyboard). */
      VolumeUp = 175,

      /** The Media Next Track key. */
      MediaNext = 176,

      /** The Media Previous Track key. */
      MediaPrevious = 177,

      /** The Media Stop key. */
      MediaStop = 178,

      /** The Media Play/Pause key. */
      MediaPlay = 179,

      /** The Start Mail key (Microsoft Natural Keyboard). */
      LaunchMail = 180,

      /** The Select Media key (Microsoft Natural Keyboard). */
      LaunchMediaSelect = 181,

      /** The Start Application 1 key (Microsoft Natural Keyboard). */
      LaunchApp1 = 182,

      /** The Start Application 2 key (Microsoft Natural Keyboard). */
      LaunchApp2 = 183,

      /** The OEM 1 key (OEM specific). */
      Oem1 = 186,

      /** The OEM Plus key on any country/region keyboard. */
      OemPlus = 187,

      /** The OEM Comma key on any country/region keyboard. */
      OemComma = 188,

      /** The OEM Minus key on any country/region keyboard. */
      OemMinus = 189,

      /** The OEM Period key on any country/region keyboard. */
      OemPeriod = 190,

      /** The OEM 2 key (OEM specific). */
      Oem2 = 191,

      /** The OEM 3 key (OEM specific). */
      Oem3 = 192,

      /** The OEM 4 key (OEM specific). */
      Oem4 = 219,

      /** The OEM 5 (OEM specific). */
      Oem5 = 220,

      /** The OEM 6 key (OEM specific). */
      Oem6 = 221,

      /** The OEM 7 key (OEM specific). */
      Oem7 = 222,

      /** The OEM 8 key (OEM specific). */
      Oem8 = 223,

      /** The OEM 102 key (OEM specific). */
      Oem102 = 226,

      /** The IME PROCESS key. */
      Process = 229,

      /** The PACKET key (used to pass Unicode characters with keystrokes). */
      Packet = 231,

      /** The ATTN key. */
      Attention = 246,

      /** The CRSEL (CURSOR SELECT) key. */
      CrSel = 247,

      /** The EXSEL (EXTEND SELECTION) key. */
      ExSel = 248,

      /** The ERASE EOF key. */
      EraseEndOfFile = 249,

      /** The PLAY key. */
      Play = 250,

      /** The ZOOM key. */
      Zoom = 251,

      /** A constant reserved for future use. */
      NoName = 252,

      /** The PA1 key. */
      Pa1 = 253,

      /** The CLEAR key (OEM specific). */
      OemClear = 254,
   }
}

namespace System {
   /**
    * Provides data for the {@link System.Console.CancelKeyPress} event. This class cannot
    * be inherited.
    */
   class ConsoleCancelEventArgs {
      /**
       * Gets or sets a value that indicates whether simultaneously pressing the
       * {@link System.ConsoleModifiers.Control} modifier key and the
       * {@link System.ConsoleKey.C} console key (Ctrl+C) or the Ctrl+Break keys terminates the
       * current process. The default is 'false', which terminates the current process.
       */
      Cancel: boolean;

      /**
       * Gets the combination of modifier and console keys that interrupted the current
       * process.
       */
      readonly SpecialKey: System.ConsoleSpecialKey;
   }
}

namespace System {
   /** Represents the SHIFT, ALT, and CTRL modifier keys on a keyboard. */
   enum ConsoleModifiers {
      /** The left or right ALT modifier key. */
      Alt = 1,

      /** The left or right SHIFT modifier key. */
      Shift = 2,

      /** The left or right CTRL modifier key. */
      Control = 4,
   }
}


namespace System {
   /**
    * Specifies combinations of modifier and console keys that can interrupt the current
    * process.
    */
   enum ConsoleSpecialKey {
      /**
       * The {@link System.ConsoleModifiers.Control} modifier key plus the
       * {@link System.ConsoleKey.C} console key.
       */
      ControlC = 0,

      /**
       * The {@link System.ConsoleModifiers.Control} modifier key plus the BREAK console key.
       */
      ControlBreak = 1,
   }
}


declare namespace Console {

   /** Gets or sets the background color of the console. */
   var BackgroundColor: System.ConsoleColor;

   /** Gets or sets the height of the buffer area. */
   var BufferHeight: number;

   /** Gets or sets the width of the buffer area. */
   var BufferWidth: number;

   /**
    * Gets a value indicating whether the CAPS LOCK keyboard toggle is turned on or turned
    * off.
    */
   const CapsLock: boolean;

   /** Gets or sets the column position of the cursor within the buffer area. */
   var CursorLeft: number;

   /** Gets or sets the height of the cursor within a character cell. */
   var CursorSize: number;

   /** Gets or sets the row position of the cursor within the buffer area. */
   var CursorTop: number;

   /** Gets or sets a value indicating whether the cursor is visible. */
   var CursorVisible: boolean;


   /** Gets or sets the foreground color of the console. */
   var ForegroundColor: System.ConsoleColor;

   /**
    * Gets a value that indicates whether the error output stream has been redirected from
    * the standard error stream.
    */
   const IsErrorRedirected: boolean;

   /**
    * Gets a value that indicates whether input has been redirected from the standard input
    * stream.
    */
   const IsInputRedirected: boolean;

   /**
    * Gets a value that indicates whether output has been redirected from the standard
    * output stream.
    */
   const IsOutputRedirected: boolean;

   /** Gets a value indicating whether a key press is available in the input stream. */
   const KeyAvailable: boolean;

   /**
    * Gets the largest possible number of console window rows, based on the current font and
    * screen resolution.
    */
   const LargestWindowHeight: number;

   /**
    * Gets the largest possible number of console window columns, based on the current font
    * and screen resolution.
    */
   const LargestWindowWidth: number;

   /**
    * Gets a value indicating whether the NUM LOCK keyboard toggle is turned on or turned
    * off.
    */
   const NumberLock: boolean;

   /** Gets or sets the title to display in the console title bar. */
   var Title: string;

   /**
    * Gets or sets a value indicating whether the combination of the
    * {@link System.ConsoleModifiers.Control} modifier key and {@link System.ConsoleKey.C}
    * console key (Ctrl+C) is treated as ordinary input or as an interruption that is
    * handled by the operating system.
    */
   var TreatControlCAsInput: boolean;

   /** Gets or sets the height of the console window area. */
   var WindowHeight: number;

   /**
    * Gets or sets the leftmost position of the console window area relative to the screen
    * buffer.
    */
   var WindowLeft: number;

   /**
    * Gets or sets the top position of the console window area relative to the screen
    * buffer.
    */
   var WindowTop: number;

   /** Gets or sets the width of the console window. */
   var WindowWidth: number;

   /** Plays the sound of a beep through the console speaker. */
   function Beep(): void;

   /**
    * Plays the sound of a beep of a specified frequency and duration through the console
    * speaker.
    */
   function Beep(
       frequency: number,
       duration: number,
   ): void;

   /** Clears the console buffer and corresponding console window of display information. */
   function Clear(): void;

   /** Gets the position of the cursor. */
   function GetCursorPosition(): [number, number];

   /**
    * Copies a specified source area of the screen buffer to a specified destination area.
    */
   function MoveBufferArea(
       sourceLeft: number,
       sourceTop: number,
       sourceWidth: number,
       sourceHeight: number,
       targetLeft: number,
       targetTop: number,
   ): void;


   /** Acquires the standard error stream. */
   function OpenStandardError(): Duplex;

   /** Acquires the standard error stream, which is set to a specified buffer size. */
   function OpenStandardError(bufferSize: number): Duplex;

   /** Acquires the standard input stream. */
   function OpenStandardInput(): Duplex;

   /** Acquires the standard input stream, which is set to a specified buffer size. */
   function OpenStandardInput(bufferSize: number): Duplex;

   /** Acquires the standard output stream. */
   function OpenStandardOutput(): Duplex;

   /** 获取设置为指定缓冲区大小的标准输出流。 */
   function OpenStandardOutput(bufferSize: number): Duplex;

   /** 从标准输入流中读取下一个字符。 */
   function Read(): number;


   /** 从标准输入流中读取下一行字符。 */
   function ReadLine(): string | undefined;

   /** 将前台和后台控制台颜色设置为默认值。 */
   function ResetColor(): void;

   /** 将屏幕缓冲区的高度和宽度设置为指定的值。 */
   function SetBufferSize(
       width: number,
       height: number,
   ): void;

   /** 设置光标的位置。 */
   function SetCursorPosition(
       left: number,
       top: number,
   ): void;


   /** 设置控制台窗口相对于屏幕缓冲区的位置。 */
   function SetWindowPosition(
       left: number,
       top: number,
   ): void;

   /** 将控制台窗口的高度和宽度设置为指定的值。 */
   function SetWindowSize(
       width: number,
       height: number,
   ): void;

   /**
    * 将指定布尔值的文本表示形式写入标准输出
    * 流。
    */
   function Write(value: boolean): void;

   /**
    * 写入指定双精度浮点值的文本表示形式
    * 到标准输出流。
    */
   function Write(value: number): void;

   /**
    * 将指定的32位带符号整数值的文本表示形式写入
    * 标准输出流。
    */
   function Write(value: number): void;

   /**
    * 将指定的64位带符号整数值的文本表示形式写入
    * 标准输出流。
    */
   function Write(value: number): void;


   /**
    * 写入指定的单精度浮点值的文本表示形式
    * 到标准输出流。
    */
   function Write(value: number): void;

   /** 将指定的字符串值写入标准输出流。 */
   function Write(value: string | undefined): void;

   /**
    * 将指定的32位无符号整数值的文本表示形式写入
    * 标准输出流。
    */
   function Write(value: number): void;

   /**
    * 将指定的64位无符号整数值的文本表示形式写入
    * 标准输出流。
    */
   function Write(value: number): void;

   /**
    * 将当前行结束符写入标准输出流。
    * */
   function WriteLine(): void;

   /**
    * 写入指定布尔值的文本表示形式，后跟当前值
    * 行结束符，到标准输出流。
    */
   function WriteLine(value: boolean): void;


   /**
    * 写入指定的双精度浮点值的文本表示形式，
    * 后跟当前行结束符，到标准输出流。
    */
   function WriteLine(value: number): void;

   /**
    * 写入指定的32位带符号整数值的文本表示形式
    * 由当前行结束符，到标准输出流。
    */
   function WriteLine(value: number): void;

   /**
    * 写入指定的64位带符号整数值的文本表示形式，后面跟着
    * 由当前行结束符，到标准输出流。
    */
   function WriteLine(value: number): void;

   /**
    * 写入指定的单精度浮点值的文本表示形式，
    * 后跟当前行结束符，到标准输出流。
    */
   function WriteLine(value: number): void;

   /**
    * 将指定的字符串值(后跟当前行结束符)写入
    * 标准输出流。
    */
   function WriteLine(value: string | undefined): void;

   /**
    * 写入指定对象的文本表示形式，后跟当前行
    * 使用指定格式信息转换为标准输出流。
    */
   function WriteLine(
       format: string,
       arg0: any | undefined,
   ): void;

   /**
    * 写入指定对象的文本表示形式，后跟当前行
    * 使用指定格式信息转换为标准输出流。
    */
   function WriteLine(
       format: string,
       arg0: any | undefined,
       arg1: any | undefined,
   ): void;

   /**
    * 写入指定对象的文本表示形式，后跟当前行
    * 使用指定格式信息转换为标准输出流。
    */
   function WriteLine(
       format: string,
       arg0: any | undefined,
       arg1: any | undefined,
       arg2: any | undefined,
   ): void;

   /**
    * 写入指定对象数组的文本表示形式，后面跟着
    * 将当前行结束符转换为使用指定格式的标准输出流
    * 信息。
    */
   function WriteLine(
       format: string,
       arg: any | undefined[] | undefined,
   ): void;

   /**
    * 写入指定的32位无符号整数值的文本表示形式，
    * 后跟当前行结束符，到标准输出流。
    */
   function WriteLine(value: number): void;

   /**
    * 写入指定的64位无符号整数值的文本表示形式，
    * 后跟当前行结束符，到标准输出流。
    */
   function WriteLine(value: number): void;
}


declare var Console: Console;