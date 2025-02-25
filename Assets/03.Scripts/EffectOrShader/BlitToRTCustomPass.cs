using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

public class BlitToRTCustomPass : CustomPass
{
    public RenderTexture renderTexture;
    private RTHandle tempRT;

    protected override void Setup(ScriptableRenderContext renderContext, CommandBuffer cmd)
    {
        tempRT = RTHandles.Alloc(renderTexture);
    } 
    
    protected override void Execute(CustomPassContext ctx)
    {
        if (renderTexture == null) return;
        
        var hdCamera = ctx.hdCamera;
        
        CoreUtils.SetRenderTarget(ctx.cmd, tempRT);
        Blitter.BlitCameraTexture(ctx.cmd, ctx.cameraColorBuffer, tempRT);
    }

    protected override void Cleanup()
    {
        tempRT?.Release();
    } 
}
