/*
 * FileName             : MYXZConfigLoader.cs
 * Author               : yqs
 * Creat Date           : 2018.3.15
 * Revision History     : 
 *          R1: 
 *              修改作者：
 *              修改日期：
 *              修改内容：
 */

using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

namespace MYXZ
{
    /// <summary>
    /// 用于获取游戏中的所有Config配置信息及Sprite
    /// </summary>
    public class MYXZConfigLoader
    {
        #region Singleton
        public static MYXZConfigLoader Instance
        {
            get { return mInstance = mInstance ?? new MYXZConfigLoader(); }
        }

        private static MYXZConfigLoader mInstance;

        private MYXZConfigLoader()
        {
        }
        #endregion

        private Dictionary<string, SceneInfo> mIdToSceneInfoDic;
        private Dictionary<string, NpcInfo> mIdToNpcInfoDic;
        private Dictionary<string, Task> mIdToTaskDic;
        private Dictionary<string, Item> mIdToItemDic;
        private Dictionary<string, Sprite> mIdToSpriteDic;
        /// <summary>
        /// 由于同一类型的角色可能存在多个并且拥有同一个技能，所以这里只存储基本的SkillNode
        /// </summary>
        private Dictionary<string, SkillRootNode> m_id2SkillNode; 
        public SaveInfo CurrentSaveInfo;

        #region InitMembers

        /// <summary>
        /// 对游戏数据做初始化
        /// </summary>
        public void Init()
        {
            mIdToNpcInfoDic = new Dictionary<string, NpcInfo>();
            mIdToTaskDic = new Dictionary<string, Task>();
            mIdToItemDic = new Dictionary<string, Item>();
            mIdToSpriteDic = new Dictionary<string, Sprite>();
        }

        private void InitSceneInfo()
        {
            mIdToSceneInfoDic = new Dictionary<string, SceneInfo>();
            AssetBundle sceneInfoAB = MYXZAssetBundleManager.Instance.LoadOrGetAssetBundle(MYXZXmlReader.GetConfigAssetBundlePath("Scene"));
            TextAsset[] sceneInfoTexts = sceneInfoAB.LoadAllAssets<TextAsset>();
            foreach (TextAsset text in sceneInfoTexts)
            {
                SceneInfo sceneInfo;
                MYXZXmlReader.ReadSceneInfo(out sceneInfo, text.text);
                mIdToSceneInfoDic.Add(sceneInfo.Id, sceneInfo);
            }
            MYXZAssetBundleManager.Instance.Unload(sceneInfoAB.name);
        }
        #endregion

        private AssetBundle GetAssetBundle(string id)
        {
            IStoreInAssetBundle storeInfo = Setting.AssetBundlePath.Type2Path[
                Setting.Config.Id2Type[Setting.Config.GetType(id)]];
            return MYXZAssetBundleManager.Instance.LoadOrGetAssetBundle(
                storeInfo.GetAssetBundlePath(Setting.Config.GetIndex(id))
            );
        }
        /// <summary>
        /// 通过ID获取Item或者装备
        /// </summary>
        /// <param name="id">Item的ID</param>
        /// <returns>对应的Item，没找到会返回null</returns>
        public Item GetItem(string id)
        {
            Item item;
            if (!this.mIdToItemDic.TryGetValue(id, out item))
            {
                AssetBundle assetBundle = GetAssetBundle(id);
                IConfigFactory<Item> factory = new ItemFactory(assetBundle);
                /*
                 * 延迟60s后卸载此AssetBundle，以防止短时间内需要大量加载此AssetBundle中的信息
                 */
                MYXZAssetBundleManager.Instance.Unload(assetBundle.name, delay:60); 

                item = factory.Create(id);
                this.mIdToItemDic.Add(id, item);
            }

            return item;
        }

        /// <summary>
        /// 通过ID获取NpcInfo
        /// </summary>
        /// <param name="id">npc的ID</param>
        /// <returns>对应的NpcInfo，没找到会返回null</returns>
        public NpcInfo GetNpcInfo(string id)
        {
            NpcInfo info;
            if (!this.mIdToNpcInfoDic.TryGetValue(id, out info))
            {
                AssetBundle assetBundle = GetAssetBundle(id);
                IConfigFactory<NpcInfo> factory = new NPCFactory(assetBundle);
                /*
                 * 延迟60s后卸载此AssetBundle，以防止短时间内需要大量加载此AssetBundle中的信息
                 */
                MYXZAssetBundleManager.Instance.Unload(assetBundle.name, delay: 60);

                info = factory.Create(id);
                this.mIdToNpcInfoDic.Add(id, info);
            }
            return info;
        }

        /// <summary>
        /// 通过ID获取Task
        /// </summary>
        /// <param name="id">task的ID</param>
        /// <returns>对应的Task，没找到会返回null</returns>
        public Task GetTask(string id)
        {
            Task task;
            if (!this.mIdToTaskDic.TryGetValue(id, out task))
            {
                AssetBundle assetBundle = GetAssetBundle(id);
                IConfigFactory<Task> factory = new TaskFactory(assetBundle);
                /*
                 * 延迟60s后卸载此AssetBundle，以防止短时间内需要大量加载此AssetBundle中的信息
                 */
                MYXZAssetBundleManager.Instance.Unload(assetBundle.name, delay: 60);

                task = factory.Create(id);
                this.mIdToTaskDic.Add(id, task);
            }
            return task;
        }

        /// <summary>
        /// 由技能ID获取对应的技能树
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public SkillRootNode GetSkillTree(string id)
        {
            //由于同一类型的角色可能存在多个并且拥有同一个技能，所以不应该使用一个Dictionary来缓存，而应该每次都创建一个新的返回
            SkillRootNode skillRootNode = null;
            if (!this.m_id2SkillNode.TryGetValue(id,  out skillRootNode))
            {
                AssetBundle assetBundle = GetAssetBundle(id);
                IConfigFactory<SkillRootNode> factory = new SkillFactory(assetBundle);

                skillRootNode = factory.Create(id);
                MYXZAssetBundleManager.Instance.Unload(assetBundle.name, delay: 60);
                this.m_id2SkillNode.Add(id, skillRootNode);
            }
            else
            {
                skillRootNode = new SkillRootNode(id);
            }

            return skillRootNode;
        }

        public SceneInfo GetSceneInfoById(string sceneId)
        {
            SceneInfo sceneInfo;
            if (this.mIdToSceneInfoDic.TryGetValue(sceneId, out sceneInfo))
            {
                return sceneInfo;
            }
            Debug.LogError("无法找到id为" + sceneId + "的SceneInfo的信息");
            return null;
        }

        /// <summary>
        /// 获得ID为spriteId的Sprite
        /// </summary>
        /// <param name="spriteId"></param>
        /// <returns></returns>
        public Sprite GetSprite(string spriteId)
        {
            if (!this.mIdToSpriteDic.ContainsKey(spriteId))
            {
                Sprite sprite = MYXZAssetBundleManager.Instance.LoadOrGetAssetBundle(MYXZXmlReader.GetConfigAssetBundlePath("ItemSprite")).LoadAsset<Sprite>(spriteId);
                this.mIdToSpriteDic.Add(spriteId, sprite);
            }
            return mIdToSpriteDic[spriteId];
        }

        public Sprite GetHeadSpriteById(string spriteId)
        {
            if (!this.mIdToSpriteDic.ContainsKey(spriteId))
            {
                Sprite sprite = MYXZAssetBundleManager.Instance.LoadOrGetAssetBundle(MYXZXmlReader.GetConfigAssetBundlePath("HeadSprite")).LoadAsset<Sprite>(spriteId);
                this.mIdToSpriteDic.Add(spriteId, sprite);
            }
            return mIdToSpriteDic[spriteId];
        }

        /// <summary>
        /// 获得所有UIPanel的配置表
        /// </summary>
        public Dictionary<UIPanelType, UIPanelInfo> GetUIConfig()
        {
            var dictionary = new Dictionary<UIPanelType, UIPanelInfo>(new UITypeCompare());
            AssetBundle uiConfigAssetBundle = MYXZAssetBundleManager.Instance.LoadOrGetAssetBundle(
                Setting.AssetBundlePath.Type2Path["UI"].GetAssetBundlePath()
                );
            TextAsset uiConfig = uiConfigAssetBundle.LoadAsset<TextAsset>("UIConfig");
            List<UIPanelInfo> uiPanelInfos = JsonConvert.DeserializeObject<List<UIPanelInfo>>(uiConfig.text);
            foreach (UIPanelInfo uiPanelInfo in uiPanelInfos)
            {
                dictionary.Add(uiPanelInfo.Type, uiPanelInfo);
            }
            MYXZAssetBundleManager.Instance.Unload(uiConfigAssetBundle.name);

            return dictionary;
        }
    }
}
