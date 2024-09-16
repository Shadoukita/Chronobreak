namespace ENet
{
    public enum PeerState
    {
        Uninitialized = -1,
        Disconnected = LENet.PeerState.DISCONNECTED,
        Connecting = LENet.PeerState.CONNECTING,
        AcknowledgingConnect = LENet.PeerState.ACKNOWLEDGING_CONNECT,
        ConnectionPending = LENet.PeerState.CONNECTION_PENDING,
        ConnectionSucceeded = LENet.PeerState.CONNECTION_SUCCEEDED,
        Connected = LENet.PeerState.CONNECTED,
        DisconnectLater = LENet.PeerState.DISCONNECT_LATER,
        Disconnecting = LENet.PeerState.DISCONNECTING,
        AcknowledgingDisconnect = LENet.PeerState.ACKNOWLEDGING_DISCONNECT,
        Zombie = LENet.PeerState.ZOMBIE,
    }
}