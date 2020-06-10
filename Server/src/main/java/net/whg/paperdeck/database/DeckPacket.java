package net.whg.paperdeck.database;

import java.io.DataOutput;
import java.io.IOException;
import net.whg.we.net.IPacketSender;
import net.whg.we.net.packets.IBinaryPacket;

public class DeckPacket implements IBinaryPacket
{
    private static final long PACKET_ID = 2363613845127168L;

    private final Deck deck;

    public DeckPacket(Deck deck)
    {
        this.deck = deck;
    }

    @Override
    public IPacketSender getSender()
    {
        return null;
    }

    @Override
    public void handle()
    {
        // Not received from client.
    }

    @Override
    public long getPacketID()
    {
        return PACKET_ID;
    }

    @Override
    public void write(DataOutput out) throws IOException
    {
        CardDatabaseIO.saveDeck(out, deck);
    }
}
