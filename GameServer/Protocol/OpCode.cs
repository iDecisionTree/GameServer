namespace GameServer.Protocol
{
    public enum OpCode : byte
    {
        Heartbeat = 0x00,

        Login = 0x01,
        LoginStatus = 0x02,
        Register = 0x03,
    }
}
