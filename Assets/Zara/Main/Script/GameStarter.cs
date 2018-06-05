﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zara.Common;
using Zara.Common.ExCamera;
using Zara.Common.ExScene;
using Zara.Common.ExBase;
using Zara.Common.Ui;
using Zara.Expansion.ExCamera;
using Zara.Expansion.ExScene;
using Zara.Expansion.Ui;
using Zara.Main.Ui;
using Zara.Main.Ui.Header;
using Zara.Main.Ui.Title;
using Zara.Expansion.ExAssetBundle;
using Zara.Common.ExAssetBundle;

namespace Zara.Main
{
    public class GameStarter : ExMonoBehaviour
    {
        public IEnumerator StartGame()
        {
            yield return PrepareFirstAssets();
            OverlayUiOperator loadingUiOperator = Game.Scene.ShowLoading();
            yield return PrepareTitleAssets();
            loadingUiOperator.AddShownAction(() => LoadTitle(loadingUiOperator));
        }

        IEnumerator PrepareFirstAssets()
        {
            yield return Game.AssetCaller.SetResrouceTable(AssetStep.First, AssetLanguage.Japanese);
            AssetBundlePreloader firstPreloader = Game.AssetCaller.GetResourcePreloader();
            firstPreloader.StartPreloading();
            while (!firstPreloader.IsFinished)
                yield return null;
        }

        IEnumerator PrepareTitleAssets()
        {
            yield return Game.AssetCaller.SetResrouceTable(AssetStep.Title, AssetLanguage.Japanese);
            AssetBundlePreloader titlePreloader = Game.AssetCaller.GetResourcePreloader();
            titlePreloader.StartPreloading();
            while (!titlePreloader.IsFinished)
                yield return null;
        }

        void LoadTitle(OverlayUiOperator loadingUiOperator)
        {
            Game.Scene.LoadTitle(ss =>
            {
                ss.Ui.AddEntriedAction(loadingUiOperator.NotifyTermination);
            });
        }
    }
}