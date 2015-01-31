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
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace JLChnToZ.Renpin {
	struct RenPinItem<T> {
		public T item;
		public float rare;
	}
	
	[Serializable]
	public class RenPinList<T> : IList<T> {
		readonly List<RenPinItem<T>> undelyingList;
		
		public RenPinList() {
			this.undelyingList = new List<RenPinItem<T>>();
		}

		public int IndexOf(T item) {
			return undelyingList.FindIndex(rpitem => rpitem.item.Equals(item));
		}

		public void Insert(int index, T item) {
			undelyingList.Insert(index, new RenPinItem<T>{ item = item, rare = 1 });
		}

		public void RemoveAt(int index) {
			undelyingList.RemoveAt(index);
		}

		public T this[int index] {
			get { return undelyingList[index].item; }
			set {
				var item = undelyingList[index];
				item.item = value;
				undelyingList[index] = item;
			}
		}

		public void Add(T item) {
			undelyingList.Add(new RenPinItem<T>{ item = item, rare = 1 });
		}

		public void Add(T item, float rare) {
			if (rare <= 0)
				throw new ArgumentOutOfRangeException("rare", rare, "Rareness must be greater than zero");
			undelyingList.Add(new RenPinItem<T>{ item = item, rare = rare });
		}
		
		public float GetRare(int index) {
			return undelyingList[index].rare;
		}
		
		public void SetRare(int index, float value) {
			if (value <= 0)
				throw new ArgumentOutOfRangeException("value", value, "Rareness must be greater than zero");
			var item = undelyingList[index];
			item.rare = value;
			undelyingList[index] = item;
		}

		public void Clear() {
			undelyingList.Clear();
		}

		public bool Contains(T item) {
			return IndexOf(item) >= 0;
		}

		public void CopyTo(T[] array, int arrayIndex) {
			undelyingList.Select(rpitem => rpitem.item).ToArray().CopyTo(array, arrayIndex);
		}

		public bool Remove(T item) {
			int index = IndexOf(item);
			if (index >= 0) {
				RemoveAt(index);
				return true;
			}
			return false;
		}

		public int Count {
			get { return undelyingList.Count; }
		}

		public bool IsReadOnly {
			get { return false; }
		}

		public IEnumerator<T> GetEnumerator() {
			return (IEnumerator<T>)undelyingList.Select(rpitem => rpitem.item).ToList();
		}

		IEnumerator IEnumerable.GetEnumerator() {
			return GetEnumerator();
		}
		
		public T LuckyDraw(ILuckier luckier) {
			return LuckyDraw(RandomGenerator.DefaultInstance, luckier);
		}
		
		public T LuckyDraw(IRandomGenerator randomGenerator, ILuckier luckier) {
			if (randomGenerator == null)
				throw new ArgumentNullException("randomGenerator");
			if (luckier == null)
				throw new ArgumentNullException("luckier");
			if (luckier.Luckyness <= 0)
				throw new ArgumentOutOfRangeException("luckier", luckier.Luckyness, "Luckyness must greater than zero.");
			T result = default(T);
			if (undelyingList.Count <= 0)
				return result;
			float randomValue = 0, enumeratedValue = 0, resultRare = 1;
			foreach (var rpitem in undelyingList)
				randomValue += luckier.Luckyness / rpitem.rare;
			randomValue *= randomGenerator.Random();
			result = undelyingList[0].item;
			resultRare = undelyingList[0].rare;
			foreach (var rpitem in undelyingList) {
				enumeratedValue += luckier.Luckyness / rpitem.rare;
				if (enumeratedValue > randomValue)
					break;
				result = rpitem.item;
				resultRare = rpitem.rare;
			}
			if(resultRare > 1)
				luckier.Luckyness /= resultRare;
			else
				luckier.Luckyness += 1 - resultRare;
			return result;
		}
	}
}
