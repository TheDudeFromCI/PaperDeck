package net.whg.paperdeck.packets;

import java.io.DataInput;
import java.io.IOException;
import net.whg.we.net.IPacketSender;
import net.whg.we.net.packets.IPacketInitializer;

/**
 * Initializes received ping packets.
 */
public class PingPacketInitializer implements IPacketInitializer<PingPacket>
{
    @Override
    public long getPacketID()
    {
        return PingPacket.PACKET_ID;
    }

    @Override
    public PingPacket loadPacket(DataInput input, IPacketSender sender) throws IOException
    {
        return new PingPacket(sender);
    }
}
