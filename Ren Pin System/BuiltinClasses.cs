/* Renpin System (Generic luckyness system)
 * (C) Jeremy Lam (JLChnToZ), 2015.
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
