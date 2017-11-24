using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class WinUtil{
    const int GWL_STYLE = -16;

    const uint WS_POPUP = 0x80000000;

    const uint WS_VISIBLE = 0x10000000;

    /// <summary>
    /// 找到对应窗口
    /// </summary>
    /// <param name="IpClassName">窗口类名，可以（建议）填空</param>
    /// <param name="IpWindowName">程序的窗口名字</param>
    /// <returns></returns>
    [DllImport("user32.dll")]
    public static extern IntPtr FindWindow(string IpClassName, string IpWindowName);

    /// <summary>
    /// 设置窗口样式
    /// </summary>
    /// https://msdn.microsoft.com/en-us/library/windows/desktop/ms632600(v=vs.85).aspx  窗体样式
    /// <param name="hwnd"></param>
    /// <param name="nlndex"></param>
    /// <param name="dwNewLong"></param>
    /// <returns></returns>
    [DllImport("user32.dll")]
    public static extern int SetWindowLong(IntPtr hwnd, int nlndex = GWL_STYLE, uint dwNewLong = WS_POPUP | WS_VISIBLE);

    /// <summary>
    /// 设置窗口显示方式（最大化、最小化）
    /// https://msdn.microsoft.com/zh-cn/library/windows/desktop/ms633548(v=vs.85).aspx
    /// SW_MAXIMIZE 3
    /// SW_MINIMIZE 6
    /// </summary>
    /// <param name="hWnd"></param>
    /// <param name="nCmdShow"></param>
    /// <returns></returns>
    [DllImport("user32.dll")]
    public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

    /// <summary>
    /// 获取窗口位置信息
    /// </summary>
    /// <param name="hWnd"></param>
    /// <param name="lpRect"></param>
    /// <returns></returns>
    [DllImport("user32.dll")]
    public static extern bool GetWindowRect(IntPtr hWnd, out WRect lpRect);

    /// <summary>
    /// 移动窗口
    /// </summary>
    /// <param name="hWnd">句柄</param>
    /// <param name="x">水平像素位置</param>
    /// <param name="y">垂直像素位置</param>
    /// <param name="nWidth">窗口宽度</param>
    /// <param name="nHeight">窗口高度</param>
    /// <param name="BRePaint">是否重绘</param>
    /// <returns></returns>
    [DllImport("user32.dll")]
    public static extern bool MoveWindow(IntPtr hWnd, int x, int y, int nWidth, int nHeight, bool BRePaint);

    /// <summary>
    /// 获取鼠标位置信息
    /// </summary>
    /// <param name="lpPoint"></param>
    /// <returns></returns>
    [DllImport("user32.dll")]
    public static extern bool GetCursorPos(out CPoint lpPoint);
}

/// <summary>
/// 窗口边缘信息结构体
/// </summary>
public struct WRect {
    public int Left;
    public int Top;
    public int Right;
    public int Bottom;


    public override String ToString() {
        return base.ToString() + "Left=" + this.Left + "  Top=" + this.Top + "  Right=" + this.Right + " Bottom=" + this.Bottom;
    }
}

/// <summary>
/// 鼠标位置结构体
/// </summary>
public struct CPoint {
    public int x;
    public int y;
    public override String ToString() {
        return base.ToString() + "x=" + this.x + "  y=" + this.y;
    }
}
