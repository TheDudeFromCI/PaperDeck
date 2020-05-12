package net.whg.paperdeck;

import net.whg.we.main.ILoopAction;
import net.whg.we.main.Timer;
import net.whg.we.main.PipelineConstants;

public class SyncFPSAction implements ILoopAction
{
    private final Timer timer;
    private final double targetFPS;
    private double smoothing;

    public SyncFPSAction(Timer timer, float targetFPS)
    {
        this.timer = timer;
        this.targetFPS = targetFPS + 0.05;
    }

    @Override
    public void run()
    {
        double delta = timer.getDeltaTime();
        double toWait = (smoothing + delta) / 2;
        smoothing = delta;

        toWait = (2 / targetFPS) - toWait;

        if (toWait > 0)
        {
            long ms = (long) (toWait * 1000);
            int ns = (int) ((toWait % 0.001) * 1.0e+9);

            sleep(ms, ns);
        }
    }

    private void sleep(long ms, int ns)
    {
        try
        {
            Thread.sleep(ms, ns);
        }
        catch (InterruptedException e)
        {}
    }

    @Override
    public int getPriority()
    {
        return PipelineConstants.FRAMERATE_LIMITER;
    }
}
