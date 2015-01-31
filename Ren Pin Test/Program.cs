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
			luckyDraw.Add("Thank you for coming", 0.1F);
			luckyDraw.Add("Diamond", 5F);
			luckyDraw.Add("Gold", 4F);
			luckyDraw.Add("Bronze", 2F);
			
			Console.WriteLine("Start a new gacha, you have {0} ren pin now.", luckier.Luckyness);
			for(int i = 0; i < 250; i++) {
				var result = luckyDraw.LuckyDraw(luckier);
				Console.WriteLine("#{0}: You got \"{1}\", now you have {2} ren pin.", i + 1, result, luckier.Luckyness);
			}
			
			Console.Write("Press any key to continue . . . ");
			Console.ReadKey(true);
		}
	}
}