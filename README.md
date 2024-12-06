# Unity_game
function test branch

###图片视频添加方法###
可以直接拖动至框内

###创造按钮逻辑方法###
1. 创建 UI Canvas
*按钮必须放在一个 Canvas 组件内，Canvas 是所有 UI 元素的容器。
在 Unity 编辑器中，右键点击 Hierarchy 窗口中的空白处。
选择 UI > Canvas 来创建一个新的 Canvas。
2. 添加按钮（Button）
*在 Canvas 内添加按钮。
右键点击 Canvas，选择 UI > Button。
这将自动在 Canvas 下创建一个名为 Button 的 UI 元素。
3. 自定义按钮样式
*你可以调整按钮的外观和文本。
在 Inspector 面板中，选中 Button 对象。
在 Button 组件中，你会看到 Text 元素。点击展开后，可以更改按钮上显示的文本。
4. 编写按钮的点击事件代码
要为按钮添加点击事件，你需要编写一个 C# 脚本，并将其绑定到按钮的 onClick 事件。
5. 将脚本与按钮连接【重要】
*将编写的脚本绑定到按钮。
将 .cs 脚本拖放到按钮对象上，或者将其放在场景中的其他 GameObject 上（EventSystem上）。
选中 Button 对象，在 Inspector 面板的 Button (Script) 组件下，找到 On Click () 事件。
点击 + 按钮添加一个新的事件监听器。
将包含 ButtonController 脚本的对象拖到事件槽中（比如拖到场景中的一个空物体上）。
在弹出的菜单中，选择 ButtonController 脚本下的 OnButtonClick 方法。

###Scene场景跳转逻辑###
1. 确保目标场景已经添加到 Build Profile
打开 Unity 的 File > Build Profile
点击 Add Open Scenes，将你的游戏场景和主界面场景都添加到列表(Scene list)中。
例如：
Scene 0: MainMenu（主界面）
Scene 1: GameScene（游戏场景）
确保场景顺序正确，并记住游戏场景的名字或索引号。
2. 编写场景跳转代码
    SceneManager.LoadScene("GameScene"); // 使用场景名称
    或者 SceneManager.LoadScene(1); // 使用场景索引
3. 参考button触发逻辑，将代码绑定到对应按钮上，点击即可跳转。