/* The MIT License (MIT)
 * 
 * Copyright (c) 2015 Jeremy Lam "JLChnToZ".
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 */
using System;
using JLChnToZ.Renpin;

namespace JLChnToZ.Renpin.Test {
	class Program {
		public static void Main(string[] args) {
			var luckier = new Luckier();
			var luckyDraw = new RenPinList<string>();
			luckyDraw.Add("謝謝惠顧", 0.1F);
			luckyDraw.Add("鴨蛋（★☆☆☆☆）", 1F);
			luckyDraw.Add("雞蛋（★★☆☆☆）", 2F);
			luckyDraw.Add("銅蛋（★★★☆☆）", 3F);
			luckyDraw.Add("銀蛋（★★★★☆）", 4F);
			luckyDraw.Add("鍍金蛋（★★★★★ Rare）", 5F);
			luckyDraw.Add("金蛋（★★★★★ Rare+）", 6F);
			luckyDraw.Add("白金蛋（★★★★★ Super Rare）", 7F);
			luckyDraw.Add("鑽石蛋（★★★★★ Super Rare+）", 8F);
			luckyDraw.Add("鑲金鑽石蛋（★★★★★ Ultra Rare）", 9F);
			luckyDraw.Add("神之蛋（★★★★★ Ultra Rare+）", 10F);
			
			Console.WriteLine("開始轉蛋活動，看看你的手氣怎樣呢？\n注：你現在有 {0:0.##} 人品。", luckier.Luckyness);
			int i = 0;
			while(true) {
				var result = luckyDraw.LuckyDraw(luckier);
				Console.WriteLine("第 {0} 抽：你獲得「{1}」，剩餘 {2:0.##} 人品。", ++i, result, luckier.Luckyness);
				var key = Console.ReadKey(true);
				if (key.Key == ConsoleKey.Escape)
					break;
			}
			
		}
	}
}