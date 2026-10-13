using UnityEngine;
using UnityEngine.Scripting;

namespace GameFrameX.Setting.Runtime
{
    /// <summary>
    /// 快手小游戏存储后端裁剪辅助器，防止 IL2CPP 代码裁剪移除存储后端类型。
    /// </summary>
    [Preserve]
    public class GameFrameXSettingKuaiShouMiniGameCroppingHelper : MonoBehaviour
    {
        [Preserve]
        private void Start()
        {
#if UNITY_WEBGL && ENABLE_KUAISHOU_MINI_GAME
            _ = typeof(KuaiShouMiniGameSettingStorage);
#endif
        }
    }
}
