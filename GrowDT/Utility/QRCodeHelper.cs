using System;
using System.Drawing;
using System.Text;
using ThoughtWorks.QRCode.Codec;

namespace GrowDT.Utility
{
    /// <summary>
    /// Description:    二维码生成
    /// 
    /// </summary>
    public static class QRCodeHelper
    {
        public static Bitmap GetQRCodeBitmap(string codeString)
        {
            int sizeNullImage = 100;
            QRCodeEncoder qrEntity = new QRCodeEncoder();
            qrEntity.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;//二维码编码方式
            qrEntity.QRCodeScale = 10;//每个小方格的宽度
            qrEntity.QRCodeVersion = 5;//二维码版本号
            qrEntity.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M;//纠错码等级

            Bitmap srcimage;
            //动态调整二维码版本号,上限40，过长返回空白图片，编码后字符最大字节长度2953
            while (true)
            {
                try
                {
                    srcimage = qrEntity.Encode(codeString, Encoding.UTF8);
                    break;
                }
                catch (IndexOutOfRangeException)
                {
                    if (qrEntity.QRCodeVersion < 40)
                    {
                        qrEntity.QRCodeVersion++;
                    }
                    else
                    {
                        srcimage = new Bitmap(sizeNullImage, sizeNullImage);
                        break;
                    }
                }
            }
            return srcimage;
        }
    }
}
