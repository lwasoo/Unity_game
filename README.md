# Unity 游戏开发指南

## 🖼️ 图片/视频添加方法
- **直接拖拽**：将媒体文件拖入Unity项目窗口的`Assets`文件夹
- **场景使用**：从`Assets`拖到Hierarchy或Scene视图

## 🕹️ 按钮创建与逻辑

### 1. 创建UI Canvas
```markdown
1. 右键点击Hierarchy空白处 → `UI > Canvas`
2. 系统会自动生成`EventSystem`（勿删）
```

### 2. 添加按钮
```markdown
1. 右键Canvas → `UI > Button`
2. 按钮包含子对象`Text`（可修改显示文字）
```

### 3. 按钮脚本控制（C#示例）
```csharp
using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour {
    public void OnButtonClick() {
        Debug.Log("按钮被点击！");
        // 在此添加自定义逻辑
    }
}
```

**绑定步骤**：
1. 将脚本挂载到任意GameObject
2. 选中按钮 → 在Inspector中找到`On Click ()`事件
3. 点击`+` → 拖入脚本所在对象
4. 选择对应方法（如`ButtonController.OnButtonClick`）

## 🔀 场景跳转管理

### 基础设置
```markdown
1. 菜单栏 `File > Build Settings`
2. 拖拽场景到`Scenes In Build`列表
   - 索引号0：主菜单场景
   - 索引号1：游戏场景
```

### 跳转代码（C#）
```csharp
using UnityEngine.SceneManagement;

// 通过名称跳转
SceneManager.LoadScene("GameScene");

// 通过索引跳转（更快）
SceneManager.LoadScene(1);
```

### 多按钮通用处理
```csharp
// 获取当前点击的按钮名称
string btnName = EventSystem.current.currentSelectedGameObject.name;

switch(btnName) {
    case "StartButton":
        // 开始游戏逻辑
        break;
    case "QuitButton":
        Application.Quit();
        break;
}
```

## 🔊 音频系统

### 背景音乐
```markdown
1. 创建空对象 → 添加`Audio Source`组件
2. 拖入音频文件到`AudioClip`
3. 勾选`Play On Awake`和`Loop`
```

### 触发式音效
```csharp
// 在需要播放音效的脚本中添加：
[SerializeField] AudioClip soundEffect;

void PlaySound() {
    AudioSource.PlayClipAtPoint(soundEffect, Camera.main.transform.position);
}
```

## ✍️ 中文字体配置

### TextMesh Pro字体导入
```markdown
1. 下载中文字体文件（.ttf）
2. 菜单栏 `Window > TextMeshPro > Font Asset Creator`
3. 关键设置：
   - Source Font: 选择字体文件
   - Character Set: 包含中文的字符集（如"Chinese"）
4. 点击`Generate Font Atlas`生成字体资产
```

## ⚠️ 跨场景数据保存
```markdown
推荐方案：
1. 使用`static`变量（简单但影响内存）
2. 创建持久化GameObject：
   - 脚本中添加 `DontDestroyOnLoad(gameObject);`
3. 使用PlayerPrefs存储简单数据
```

## 📦 项目结构建议
```
Assets/
├─ Audio/
├─ Fonts/
├─ Prefabs/
├─ Scenes/
│  ├─ 0_MainMenu.unity
│  └─ 1_GameScene.unity
└─ Scripts/
```

---

