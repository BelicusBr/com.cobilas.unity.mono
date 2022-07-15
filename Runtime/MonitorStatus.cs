using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
#if UNITY_2018_1_OR_NEWER
using UnityEngine.Profiling;
#endif

namespace Cobilas.Unity.Mono {
    [ExecuteInEditMode]
    public class MonitorStatus : MonoBehaviour {

        [SerializeField, Range(.1f, 2f)]
        private float updateInterval = 1f;
        [SerializeField] private bool fpsMode = true;
        [SerializeField] private bool pt_br = true;
        [SerializeField] private bool activeMonitor = true;

        private MonitorInfo Info;

        private static MonitorStatus mainMonitorStatus = (MonitorStatus)null;
        public static MonitorStatus MainMonitorStatus => mainMonitorStatus;

        private void Awake() {

            if (mainMonitorStatus == (MonitorStatus)null) {
                mainMonitorStatus = this;
#if UNITY_EDITOR
                if (EditorApplication.isPlaying)
                    DontDestroyOnLoad(gameObject);
#else
                DontDestroyOnLoad(gameObject);
#endif
            }
            else { 
                if (mainMonitorStatus != this)
                    DestroyImmediate(gameObject);
            }
        }

        private void Update()
            => Monitor(updateInterval, pt_br, fpsMode, Info, out Info);

        private void OnGUI() {
            Rect m_pos = new Rect(0, 0, pt_br ? 320 : 250, 25);
#if !UNITY_EDITOR
            activeMonitor = true;
#else
            activeMonitor = GUI.Toggle(m_pos, activeMonitor, pt_br ? "Ativar monitor" : "Active monitor");
            m_pos = MoveDown(m_pos);
#endif
            if (!activeMonitor) return;
            fpsMode = GUI.Toggle(m_pos, fpsMode, pt_br ? "Modo FPS" : "FPS mode");
            GUI.Box(m_pos = MoveDown(m_pos), Info.typeMonitor);
            GUI.Box(m_pos = MoveDown(m_pos), Info.frameinfo);
            GUI.Box(m_pos = MoveDown(m_pos), Info.bestFramesinfo);
            GUI.Box(m_pos = MoveDown(m_pos), Info.frameMediainfo);
            GUI.Box(m_pos = MoveDown(m_pos), Info.worstPicturesinfo);
            GUI.Box(m_pos = MoveDown(m_pos), Info.memoryinfo);
            GUI.Box(m_pos = MoveDown(m_pos), Info.gpumemoryinfo);
            GUI.Box(m_pos = MoveDown(m_pos), Info.TotalAllocatedMemoryinfo);
            GUI.Box(m_pos = MoveDown(m_pos), Info.TotalReservedMemoryinfo);
            GUI.Box(m_pos = MoveDown(m_pos), Info.TotalUnusedReservedMemoryinfo);
#if UNITY_EDITOR
            GUI.Box(m_pos = MoveDown(m_pos), Info.DrawCallsinfo);
            GUI.Box(m_pos = MoveDown(m_pos), Info.UsedTextureMemoryinfo);
            GUI.Box(m_pos = MoveDown(m_pos), Info.RenderedTextureCountinfo);
#endif

            if (GUI.Button(MoveDown(m_pos), pt_br ? "Sair" : "Quit"))
                Application.Quit();
        }

        private Rect MoveDown(Rect pos) {
            pos.y += 25;
            return pos;
        }

        public static void Monitor(float updateInterval, bool pt_br, bool fpsMode, MonitorInfo IN_monitor, out MonitorInfo OUT_monitor) {
            float unscaledDeltaTime = Time.unscaledDeltaTime;

            IN_monitor.frames++;
            IN_monitor.duration += unscaledDeltaTime;

            if (IN_monitor.duration >= updateInterval)
            {
                IN_monitor.frameMedia = IN_monitor.frames / IN_monitor.duration;
                IN_monitor.duration = IN_monitor.worstPictures = IN_monitor.frames = 0;
                IN_monitor.bestFrames = float.MaxValue;
            }

            if (unscaledDeltaTime > IN_monitor.bestFrames)
                IN_monitor.bestFrames = unscaledDeltaTime;

            if (unscaledDeltaTime < IN_monitor.worstPictures)
                IN_monitor.worstPictures = unscaledDeltaTime;

            float _frames = 1f / unscaledDeltaTime;
            float _bestFrames = 1f / unscaledDeltaTime;
            float _worstPictures = 1f / unscaledDeltaTime;

            _frames = fpsMode ? (int)_frames : 1000f * _frames;
            _bestFrames = fpsMode ? (int)_bestFrames : 1000f * _bestFrames;
            _worstPictures = fpsMode ? (int)_worstPictures : 1000f * _worstPictures;
            float _frameMedia = fpsMode ? (int)IN_monitor.frameMedia : 1000f * IN_monitor.frameMedia;

            IN_monitor.typeMonitor = $"{(pt_br ? "Tipo de monitor" : "monitor type")} : {(fpsMode ? "FPS" : "MS")}";
            IN_monitor.frameinfo = $"{(pt_br ? "quadros" : "frames")} : {_frames}";
            IN_monitor.bestFramesinfo = $"{(pt_br ? "melhores quadros" : "best frames")} : {_bestFrames}";
            IN_monitor.frameMediainfo = $"{(pt_br ? "media de quadros" : "frame media")} : {_frameMedia}";
            IN_monitor.worstPicturesinfo = $"{(pt_br ? "piores quadros" : "worst pictures")} : {_worstPictures}";
            IN_monitor.memoryinfo = $"{(pt_br ? "Tamanho da vRam" : "graphics memory size")} : {SystemInfo.graphicsMemorySize}";
            IN_monitor.gpumemoryinfo = $"{(pt_br ? "Tamanho da Ram" : "system memory size")} : {SystemInfo.systemMemorySize}";
            IN_monitor.TotalAllocatedMemoryinfo = $"{(pt_br ? "total de memória alocada" : "total allocated memory")} : {Profiler.GetTotalAllocatedMemoryLong() / 1048576} mb";
            IN_monitor.TotalReservedMemoryinfo = $"{(pt_br ? "total de memória reservada" : "total reserved memory")} : {Profiler.GetTotalReservedMemoryLong() / 1048576} mb";
            IN_monitor.TotalUnusedReservedMemoryinfo = $"{(pt_br ? "total de memória reservada não utilizada" : "total unused reserved memory")} : {Profiler.GetTotalUnusedReservedMemoryLong() / 1048576} mb";
#if UNITY_EDITOR
            IN_monitor.DrawCallsinfo = $"{(pt_br ? "chamadas de desenho" : "draw calls")} : {UnityStats.drawCalls}";
            IN_monitor.UsedTextureMemoryinfo = $"{(pt_br ? "tamanho de memória de textura usada" : "used texture memory size")} : {UnityStats.usedTextureMemorySize / 1048576}";
            IN_monitor.RenderedTextureCountinfo = $"{(pt_br ? "quantas texturas renderizadas" : "render texture count")} : {UnityStats.renderTextureCount}";
#endif
            OUT_monitor = IN_monitor;
        }

        public struct MonitorInfo {
            public string typeMonitor;
            public string frameinfo;
            public string bestFramesinfo;
            public string frameMediainfo;
            public string worstPicturesinfo;
            public string memoryinfo;
            public string gpumemoryinfo;
            public string TotalAllocatedMemoryinfo;
            public string TotalReservedMemoryinfo;
            public string TotalUnusedReservedMemoryinfo;
#if UNITY_EDITOR
            public string DrawCallsinfo;
            public string UsedTextureMemoryinfo;
            public string RenderedTextureCountinfo;
#endif
            public int frames;
            public float duration;
            public float bestFrames;
            public float frameMedia;
            public float worstPictures;
        }
    }
}
