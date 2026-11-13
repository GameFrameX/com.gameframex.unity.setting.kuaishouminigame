namespace GameFrameX.Setting.Runtime
{
    /// <summary>
    /// 快手小游戏存档文件存储后端。基于快手 FileSystemManager 同步接口：用户目录 <c>KSBase.env.USER_DATA_PATH</c>，两阶段原子写经 WriteFileSync + CopyFileSync + UnlinkSync。
    /// </summary>
    internal sealed class KuaiShouMiniGameSaveSlotFileStorage : MiniGameSaveSlotFileStorageBackendBase
    {
        [UnityEngine.RuntimeInitializeOnLoadMethod(UnityEngine.RuntimeInitializeLoadType.SubsystemRegistration)]
        private static void Register()
        {
            SaveSlotFileStorageBackendFactory.RegisterChannelBackend(() => new KuaiShouMiniGameSaveSlotFileStorage());
        }

        protected override string GetPlatformUserDataPath()
        {
            return KSWASM.KSBase.env.USER_DATA_PATH;
        }

        protected override string CombinePath(string left, string right)
        {
            return left + "/" + right;
        }

        protected override void WriteFileBytes(string filePath, byte[] contents)
        {
            KSWASM.KSBase.GetFileSystemManager().WriteFileSync(filePath, contents);
        }

        protected override byte[] ReadFileBytes(string filePath)
        {
            return KSWASM.KSBase.GetFileSystemManager().ReadFileSync(filePath);
        }

        protected override void CopyFileBytes(string sourceFilePath, string destinationFilePath)
        {
            KSWASM.KSBase.GetFileSystemManager().CopyFileSync(sourceFilePath, destinationFilePath);
        }

        protected override void DeleteFile(string filePath)
        {
            KSWASM.KSBase.GetFileSystemManager().UnlinkSync(filePath);
        }

        protected override bool ExistsFile(string filePath)
        {
            return KSWASM.KSBase.GetFileSystemManager().AccessSync(filePath).Equals("access:ok");
        }

        protected override void CreateDirectoryIfExistsMissing(string directoryPath)
        {
            if (KSWASM.KSBase.GetFileSystemManager().AccessSync(directoryPath).Equals("access:ok"))
            {
                return;
            }

            KSWASM.KSBase.GetFileSystemManager().MkdirSync(directoryPath, true);
        }

        protected override string[] ListDirectoryEntryNames(string directoryPath)
        {
            return KSWASM.KSBase.GetFileSystemManager().ReaddirSync(directoryPath);
        }
    }
}
