using Helper.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Windows.System;
using WindowsHook;
using WindowsInput;

namespace Helper.Views.Utils {

    public class MacroRunner {

        private readonly static double MACRO_INTERVAL_MS = 10;
        private static MacroRunner MACRO_INSTANCE = new MacroRunner();

        private Timer _timer;

        private volatile bool _keybindPressed = false;

        private InputSimulator _simulator = new InputSimulator();
        private VirtualKey _keybind = VirtualKey.None;
        private ScrollDirection _scrollDirection = ScrollDirection.NONE;
        private IKeyboardMouseEvents _hook;

        public enum ScrollDirection {
            NONE,
            SCROLL_UP,
            SCROLL_DOWN
        }

        private MacroRunner() {
            _timer = new Timer();
            _timer.Interval = MACRO_INTERVAL_MS;
            _timer.Elapsed += OnTimedEvent;
            _timer.AutoReset = true;
        }

        private void OnTimedEvent(Object source, ElapsedEventArgs e) {
            if (!_keybindPressed) {
                return;
            }
            if (_scrollDirection == ScrollDirection.SCROLL_UP) {
                _simulator.Mouse.VerticalScroll(1);
            } else if (_scrollDirection == ScrollDirection.SCROLL_DOWN) {
                _simulator.Mouse.VerticalScroll(-1);
            }
        }

        public static void EnableMacro() {
            MACRO_INSTANCE.Enable();
        }

        public static void DisableMacro() {
            MACRO_INSTANCE.Disable();
        }

        public static void SetKeybind(VirtualKey keybind) {
            MACRO_INSTANCE._keybind = keybind;
        }

        public static VirtualKey GetKeybind() {
            return MACRO_INSTANCE._keybind;
        }

        public static void SetScrollDirection(ScrollDirection scrollDirection) {
            MACRO_INSTANCE._scrollDirection = scrollDirection;
        }

        private void Enable() {
            if (_hook == null) {
                _hook = Hook.GlobalEvents();
                _hook.MouseDownExt += GlobalHookMouseDownExt;
                _hook.KeyDown += GlobalHookKeyDown;
                _hook.KeyUp += GlobalHookKeyUp;
            }
            _timer.Enabled = true;
        }

        private void Disable() {
            if (_hook != null) {
                _hook.Dispose();
                _hook = null;
                _keybindPressed = false;
            }
            _timer.Enabled = false;
        }

        private void GlobalHookKeyDown(object sender, KeyEventArgs e) {
            if (_keybindPressed) {
                return;
            }
            String key = e.KeyCode.ToString().ToLower();
            String keybind = _keybind.ToString().ToLower();
            if (key.Contains(keybind) || keybind.Contains(key)) {
                _keybindPressed = true;
            }
        }

        private void GlobalHookKeyUp(object sender, KeyEventArgs e) {
            if (!_keybindPressed) {
                return;
            }
            String key = e.KeyCode.ToString().ToLower();
            String keybind = _keybind.ToString().ToLower();
            if (key.Contains(keybind) || keybind.Contains(key)) {
                _keybindPressed = false;
            }
        }

        private void GlobalHookMouseDownExt(object sender, MouseEventExtArgs e) {
        }
    }
}
