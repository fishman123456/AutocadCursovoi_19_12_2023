namespace ACADCommands
{
    public class ClassAttrB
    {
        public int id { get; set; }
        public string BlockHandle { get; set; } = "BlockHandle";
        public string BlockName { get; set; } = "BlockName";
        public string BlockAttrVal { get; set; } = "BlockAttrVal";
        public string BlockX { get; set; } = "BlockX";
        public string BlockY { get; set; } = "BlockY";
        public string BlockZ { get; set; } = "BlockZ";
        public string BlockLayer { get; set; } = "BlockLayer";
        public ClassAttrB() { }
        public ClassAttrB(string BH, string BN, string BAV, string BX, string BY, string BZ, string BL)
        {
            BlockHandle = BH;
            BlockName = BN;
            BlockAttrVal = BAV;
            BlockX = BX;
            BlockY = BY;
            BlockZ = BZ;
            BlockLayer = BL;
        }

    }
}
