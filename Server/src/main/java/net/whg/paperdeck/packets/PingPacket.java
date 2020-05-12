package net.whg.paperdeck.packets;

import java.io.DataOutput;
import net.whg.we.net.IPacketSender;
import net.whg.we.net.packets.IBinaryPacket;

public class PingPacket implements IBinaryPacket
{
    @Override
    public IPacketSender getSender()
    {
        // TODO Auto-generated method stub
        return null;
    }

    @Override
    public void handle()
    {
        // TODO Auto-generated method stub
    }

    @Override
    public long getPacketID()
    {
        // TODO Auto-generated method stub
        return 0;
    }

    @Override
    public void write(DataOutput arg0)
    {
        // TODO Auto-generated method stub

    }

}
