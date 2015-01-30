/* Renpin System (Generic luckyness system)
 * (C) Jeremy Lam (JLChnToZ), 2015.
 */
using System;

namespace JLChnToZ.Renpin {
	public interface ILuckier {
		float Luckyness { get; set; }
	}
	
	public interface IRandomGenerator {
		float Random();
	}
}
