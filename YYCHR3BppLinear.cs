using System;
using CharactorLib.Common;
using CharactorLib.Format;
using System.Drawing;

namespace YYCHR3BppLinear
{
    public class _3BppLinearFormat : FormatBase
    {
        public _3BppLinearFormat()
        {
            base.FormatText = "[8][8]";
            base.Name = "3BPP Linear (SMW Mode 7)";
            base.Extension = "smc,sfc,fig";
            base.Author = "Mattrizzle";
            base.Url = "https://github.com/Mattrizzle/YYCHR-3BppLinearPlugin";
            // Flags
            base.Readonly = false;
            base.IsCompressed = false;
            base.EnableAdf = true;
            base.IsSupportMirror = false;
            base.IsSupportRotate = false;    
            // Settings
            base.ColorBit = 3;
            base.ColorNum = 8;
            base.CharWidth = 8;
            base.CharHeight = 8;
            // Settings for Image Convert
            base.Width = 128;
            base.Height = 128;
        }

        // data: contains image data in source format (ROM)
        // addr: contains source address
        // bytemap: contains image data in YYCHR internal format
        // px: contains x coordinate of current pixel
        // py: contains y coordinate of current pixel

        /* convert from source format (ROM) to YYCHR graphics, one tile at a time */
        public override void ConvertMemToChr(Byte[] data, int addr, Bytemap bytemap, int px, int py)
        {
            for (int x = 0; x < CharWidth; x++)
            {
                for (int y = 0; y < CharHeight; y++)
                {
                    int pixel = 0x00;
                    int byteIndex = y * 3;
                    int byte0 = data[addr + byteIndex];
                    int byte1 = data[addr + byteIndex + 1];
                    int byte2 = data[addr + byteIndex + 2];
                    int b = ( byte0 << 16) ^ ( byte1 << 8 ) ^ byte2;

                    /* get pixel address for requested coordinates in YYCHR bitmap */
                    Point p = base.GetAdvancePixelPoint(px + x, py + y);
                    int bytemapAddr = bytemap.GetPointAddress(p.X, p.Y);

                    // gather bits comprising pixel
                    pixel = b >> 3 * (7 - (x % 8)) & 7;

                    /* write pixel to YYCHR bitmap */
                    bytemap.Data[bytemapAddr] = (byte) pixel;
                }
            }
        }

        /* convert from YYCHR graphics to source format (ROM), one tile at a time */
        public override void ConvertChrToMem(Byte[] data, int addr, Bytemap bytemap, int px, int py)
        {
            for (int x = 0; x < CharWidth; x++)
            {
                for (int y = 0; y < CharHeight; y++)
                {
                    // get pixel from YYCHR bitmap
                    Point p = base.GetAdvancePixelPoint(px + x, py + y);
                    int bytemapAddr = bytemap.GetPointAddress(p.X, p.Y);
                    int pixel = bytemap.Data[bytemapAddr];

                    // calculate the byte index for this pixel
                    int byteIndex = y * 3;

                    // start fresh if it's a new byte, otherwise grab the existing data
                    int byte0 = data[addr + byteIndex];
                    int byte1 = data[addr + byteIndex + 1];
                    int byte2 = data[addr + byteIndex + 2];
                    int b = x % 8 == 0 ? 0x00 : (byte0 << 16) ^ (byte1 << 8) ^ byte2;

                    b |= pixel << 3 * (7 - (x % 8));

                    // write pixel to YYCHR bytemap
                    data[addr + byteIndex] = (byte)((b >> 16) & 0xFF);
                    data[addr + byteIndex + 1] = (byte)((b >> 8) & 0xFF);
                    data[addr + byteIndex + 2] = (byte)(b & 0xFF);
                }
            }
        }
    }
}
