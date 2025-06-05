《流光与君书》

2025计算机设计大赛-数媒游戏组游戏设计作品

---

目录

- [项目简介](#项目简介)
- [功能特性](#功能特性)
- [效果截图](#效果截图)
- [环境与依赖](#环境与依赖)
- [快速开始](#快速开始)
  - [克隆仓库](#克隆仓库)
  - [导入 Unity](#导入-unity)
  - [运行方式](#运行方式)
- [项目结构](#项目结构)
- [开发者信息](#开发者信息)
- [版权与协议](#版权与协议)

---

## 项目简介

《流光与君书》是一款融合历史穿越与物理模拟的3D沉浸式交互体验游戏。玩家扮演一名现代物理学爱好者，机缘巧合下穿越至古代，与中国历史上的物理学学者、工匠等共同解决物理难题。

---

## 功能特性

核心玩法包含：  
①多线程对话系统：动态分支对话，玩家可以通过与古代物理学家的对话逐渐揭示他们的思想，玩家的选择和实验进度会影响剧情走向，从而解锁不同的对话和结局。
②物理引擎模拟：通过拖拽、点击、键盘输入等交互方式，玩家可以操控不同的物体，模拟现实世界中的物理现象；  
③跨时空解谜：玩家在游戏过程中穿梭时空裂缝与古人共同解谜，通过特定的对话和物理实验，结合古代智慧与现代科学知识共同解决难题；
④认识古代物理学先驱：通过与NPC的互动，玩家可以了解中国古代物理学家的故事。通过完成任务或解锁对话，玩家将学习科学理论和技术，不仅是学习知识，还能通过实际操作体验这些科学原理的应用。

---

## 效果截图

![主场景预览](docs/images/main_scene.png)  
*图 1：游戏主场景*

![对话示例](docs/images/dialog_example.png)  
*图 2：与NPC对话界面*

---

## 环境与依赖

工具名称	              类型	        用途说明	                                                            许可证类型
Unity                 用于实现物理模拟、三维场景渲染、角色控制、交互逻辑等功能。                             Unity 软件许可协议（非开源，遵守其使用条款）
Blender	              3D建模软件	    用于制作/修改部分游戏中的3D模型资源，如古亭、太湖石、实验道具、建筑等。	  开源（GNU GPL v2+）
Adobe After Effects	  动画/后期制作	用于制作开场动画、过场视频、墨滴特效、时间扭曲视觉表现等。	              商业授权（Adobe 订阅）

第三方 SDK/插件： 
  - Cinemachine（用于第一人称镜头控制）

---

## 快速开始

### 克隆仓库

---

## 项目结构

land2/
├── Assets/                  # Unity 项目资源目录
│   ├── Scenes/              # 包含所有场景文件（.unity）
│   ├── Scripts/             # C# 脚本文件夹
│   │   ├── Gameplay/        # 物理解谜逻辑脚本
│   │   ├── UI/              # UI 交互脚本
│   │   └── Manager/         # 游戏管理器，如场景加载、进度控制
│   ├── Prefabs/             # 预制体
│   ├── Models/              # 3D 模型资源（.fbx 等）
│   ├── Textures/            # 贴图资源（.png/.jpg 等）
│   └── UI/                  # Canvas、按钮等 UI 资源
│
├── Packages/                # Unity Package 管理的依赖
│   └── manifest.json        # 包装列表
│
├── ProjectSettings/         # Unity 项目全局配置
│   ├── ProjectSettings.asset
│   └── InputManager.asset   # 输入映射
│
├── .gitignore               # Git 忽略规则
├── README.md                # 本自述文件
└── LICENSE                  # 项目开源协议（可选）


---

## 开发者信息

开发者：佘钊莉（安徽大学 软件工程 2022级）

GitHub 个人主页：https://github.com/308321

---

## 版权与协议

本项目采用 MIT License，详情请参见 LICENSE 文件。

以下为简要许可说明：

MIT License

Copyright (c) 2025 佘钊莉

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the “Software”), to deal
in the Software without restriction, including without limitation the rights
...

允许任何人免费使用、复制、修改并分发本项目，只要保留本许可声明。

对本项目或其衍生作品不提供任何担保，也不承担任何责任。

---

## 常见问题（FAQ）

- SampleScene场景中物体贴图丢失/异常

  - 在项目的Chinese Painting URP的Material或Texture找到相应材料或贴图，展开预制体，拖入即可

- Unity 异常

这通常是因为你的本地 Unity 版本和项目保存时的版本不同，建议升级（或降级）到 2020.3.18f1 LTS。

如果升级到更高版本，请在打开时选择“Upgrade”并等待 Unity 自动完成所有资源重新导入。

如何贡献代码或提交 Issue

欢迎在本仓库的 Issues 中提交 Bug 报告或功能建议。请在 Issue 标题中简要描述问题，并附上重现步骤。

如要贡献代码，请先 Fork 本仓库，在 Fork 出来的项目中创建分支进行修改，提交完成后发起 Pull Request，我们会及时进行 Code Review。


