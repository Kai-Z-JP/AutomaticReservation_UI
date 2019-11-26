using System;

public static class ExecutionState
{
    [System.Runtime.InteropServices.DllImport("kernel32.dll")]
    static extern EXECUTION_STATE SetThreadExecutionState(EXECUTION_STATE esFlags);

    [FlagsAttribute]
    public enum EXECUTION_STATE : uint
    {
        // スタンバイ状態にするのを防ぐ
        ES_SYSTEM_REQUIRED = 0x00000001,

        // ディスプレイをオフにするのを防ぐ
        ES_DISPLAY_REQUIRED = 0x00000002,

        // 実行状態を維持する 
        ES_CONTINUOUS = 0x80000000,
    }

    // スタンバイ防止
    public static EXECUTION_STATE DisableSuspend()
    {
        return SetThreadExecutionState(
            EXECUTION_STATE.ES_SYSTEM_REQUIRED |
            EXECUTION_STATE.ES_CONTINUOUS);
    }

    // スタンバイ防止を解除
    public static EXECUTION_STATE EnableSuspend()
    {
        return SetThreadExecutionState(
            EXECUTION_STATE.ES_CONTINUOUS);
    }
}