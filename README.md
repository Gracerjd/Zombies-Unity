# 丧尸围城 · 俯视角生存射击

> Unity 2023.2 实现的 Wave 生存射击，含波次生成、武器系统、敌人 AI。

![screenshot]()

## 玩法
WASD 移动，鼠标瞄准射击，撑过每波敌人……

## 我做了什么
- 波次生成与难度曲线（`Assets/Scripts/WaveManager.cs`）
- 敌人寻路与状态机（`Assets/Scripts/Enemy/`）
- 武器切换与后坐力（`Assets/Scripts/Weapons/`）

## 技术点
- 对象池复用子弹，避免 GC 抖动
- NavMesh 动态避障
- ScriptableObject 配置武器数据

## 如何运行
Unity 2023.2.20f1c1 打开，素材需自行从 Asset Store 导入（见下）
