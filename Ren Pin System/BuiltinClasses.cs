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
using System.Security.Cryptography;

namespace JLChnToZ.Renpin {
	public sealed class Luckier: ILuckier {
		float luckyness;
		
		public float Luckyness {
			get { return luckyness; }
			set {
				if (value <= 0)
					throw new ArgumentOutOfRangeException("value", value, "Luckyness must be greater than zero");
				luckyness = value;
			}
		}
		
		public Luckier() {
			luckyness = 1;
		}
		
		public Luckier(float defaultValue) {
			if (defaultValue <= 0)
				throw new ArgumentOutOfRangeException("defaultValue", defaultValue, "Luckyness must be greater than zero");
			Luckyness = defaultValue;
		}
	}
	
	public sealed class RandomGenerator: IRandomGenerator {
		readonly static RandomGenerator defaultInstance;
		readonly RandomNumberGenerator generator;
		
		public static RandomGenerator DefaultInstance {
			get { return defaultInstance; }
		}
		
		public RandomNumberGenerator UndelyingGenerator {
			get { return generator; }
		}
		
		static RandomGenerator() {
			defaultInstance = new RandomGenerator();
		}
		
		public RandomGenerator() {
			this.generator = new RNGCryptoServiceProvider();
		}
		
		public RandomGenerator(RandomNumberGenerator generator) {
			this.generator = generator;
		}
		
		public float Random() {
			var byteValue = new byte[4];
			generator.GetBytes(byteValue);
			return (float)BitConverter.ToUInt32(byteValue, 0) / UInt32.MaxValue;
		}
		
	}
	
}
