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
using UnityEngine.SceneManagement;

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

        private Dictionary<string, SceneInfo> m_id2SceneInfoDic;
        private Dictionary<string, NpcInfo> m_id2NpcInfoDic;
        private Dictionary<string, Task> m_id2TaskDic;
        private Dictionary<string, Item> m_id2ItemDic;
        private Dictionary<string, Sprite> m_id2SpriteDic;
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
            this.m_id2NpcInfoDic = new Dictionary<string, NpcInfo>();
            this.m_id2TaskDic = new Dictionary<string, Task>();
            this.m_id2ItemDic = new Dictionary<string, Item>();
            this.m_id2SpriteDic = new Dictionary<string, Sprite>();
            this.m_id2SkillNode = new Dictionary<string, SkillRootNode>();
        }
        #endregion

        private AssetBundle GetAssetBundle(string id)
        {
            IStoreInAssetBundle storeInfo = Setting.AssetBundlePath.Type2Path[
                Setting.Config.Id2Type[Setting.Config.GetType(id)]];
            DebugHelper.Assertion(storeInfo == null, 
                "目标ID为" + id + "的config不存在或者不存储在AssetBundle中");
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
            if (!this.m_id2ItemDic.TryGetValue(id, out item))
            {
                AssetBundle assetBundle = GetAssetBundle(id);
                IConfigFactory<Item> factory = new ItemFactory(assetBundle);
                /*
                 * 延迟60s后卸载此AssetBundle，以防止短时间内需要大量加载此AssetBundle中的信息
                 */
                MYXZAssetBundleManager.Instance.Unload(assetBundle.name, delay:60); 

                item = factory.Create(id);
                DebugHelper.Assertion(item == null, "目标ID为" + id + "的Item不存在");
                this.m_id2ItemDic.Add(id, item);
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
            if (!this.m_id2NpcInfoDic.TryGetValue(id, out info))
            {
                AssetBundle assetBundle = GetAssetBundle(id);
                IConfigFactory<NpcInfo> factory = new NPCInfoFactory(assetBundle);
                /*
                 * 延迟60s后卸载此AssetBundle，以防止短时间内需要大量加载此AssetBundle中的信息
                 */
                MYXZAssetBundleManager.Instance.Unload(assetBundle.name, delay: 60);

                info = factory.Create(id);
                DebugHelper.Assertion(info == null, "目标ID为" + id + "的NPCInfo不存在");
                this.m_id2NpcInfoDic.Add(id, info);
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
            if (!this.m_id2TaskDic.TryGetValue(id, out task))
            {
                AssetBundle assetBundle = GetAssetBundle(id);
                IConfigFactory<Task> factory = new TaskFactory(assetBundle);
                /*
                 * 延迟60s后卸载此AssetBundle，以防止短时间内需要大量加载此AssetBundle中的信息
                 */
                MYXZAssetBundleManager.Instance.Unload(assetBundle.name, delay: 60);

                task = factory.Create(id);
                DebugHelper.Assertion(task == null, "目标ID为" + id + "的Task不存在");
                this.m_id2TaskDic.Add(id, task);
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
                DebugHelper.Assertion(skillRootNode == null, "目标ID为" + id + "的Skill不存在");
                MYXZAssetBundleManager.Instance.Unload(assetBundle.name, delay: 60);
                //this.m_id2SkillNode.Add(id, skillRootNode);
            }
            else //TODO 每次需要某个技能时应该返回一份copy
            {
                skillRootNode = new SkillRootNode(id);
            }

            return skillRootNode;
        }

        public SceneInfo GetSceneInfo(string id)
        {
            SceneInfo sceneInfo;
            if (!this.m_id2SceneInfoDic.TryGetValue(id, out sceneInfo))
            {
                AssetBundle assetBundle = GetAssetBundle(id);
                IConfigFactory<SceneInfo> factory = new SceneInfoFactory(assetBundle);

                sceneInfo = factory.Create(id);
                MYXZAssetBundleManager.Instance.Unload(assetBundle.name);
                DebugHelper.Assertion(sceneInfo == null, "目标ID为" + id + "的Scene不存在");
                this.m_id2SceneInfoDic.Add(id, sceneInfo);
            }
            return sceneInfo;
        }

        /// <summary>
        /// 获得ID为spriteId的Sprite
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Sprite GetSprite(string id)
        {
            if (!this.m_id2SpriteDic.ContainsKey(id))
            {
                AssetBundle assetBundle = GetAssetBundle(id);

                IConfigFactory<Sprite> factory = new SpriteFactory(assetBundle);
                Sprite sprite = factory.Create(id);
                DebugHelper.Assertion(sprite == null, "目标ID为" + id + "的Sprite不存在");
                this.m_id2SpriteDic.Add(id, sprite);
            }
            return m_id2SpriteDic[id];
        }

        public Sprite GetHeadSpriteById(string spriteId)
        {
            if (!this.m_id2SpriteDic.ContainsKey(spriteId))
            {
                Sprite sprite = MYXZAssetBundleManager.Instance.LoadOrGetAssetBundle(MYXZXmlReader.GetConfigAssetBundlePath("HeadSprite")).LoadAsset<Sprite>(spriteId);
                this.m_id2SpriteDic.Add(spriteId, sprite);
            }
            return m_id2SpriteDic[spriteId];
        }

        /// <summary>
        /// 获得所有UIPanel的配置表
        /// </summary>
        /// <returns>UI的信息</returns>
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
