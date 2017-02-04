using System;
using System.Collections.Generic;

namespace PassGen
{
	public class PassGenerator
	{
		private Boolean IS_UPPER_CASE = false; 
		private Boolean IS_LOWER_CASE = false;
		private Boolean IS_SYMBOLS_CHECK = false;
		private Boolean IS_NUMBER_CHECK = false;
		private int WORD_COUNT = 0;
		private Random random = new Random();
		private int LENG = 0;

		protected LinkedList<char> WORD_DIC = new LinkedList<char>();

		public void initialize(Boolean IS_UPPER_CASED,Boolean IS_LOWER_CASED,Boolean IS_SYMBOLS_CHECKED, Boolean IS_NUMBER_CHECKED,int word_count){
			this.IS_UPPER_CASE = IS_UPPER_CASED;
			this.IS_LOWER_CASE = IS_LOWER_CASED;
			this.IS_SYMBOLS_CHECK = IS_SYMBOLS_CHECKED;
			this.IS_NUMBER_CHECK = IS_NUMBER_CHECKED;
			this.WORD_COUNT = word_count;
		}

		public String getSequence(){
			return generateSequence();
		}

		protected String generateSequence(){
			String result = "";

			int next = 0;
			String lowerString = "abcdefghijklmnopqrstuvwxyz";
			String upperString = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
			String numberString = "1234567890";
			String symbolString = "`~!#$%^&*()-_=+[{]};:,<.>?";
			String combinedStrings = "";
		  

			if(IS_LOWER_CASE){
				combinedStrings += lowerString;
			}

			if(IS_UPPER_CASE){ 
				combinedStrings += upperString;
			}

			if(IS_SYMBOLS_CHECK){
				combinedStrings += symbolString;
			}

			if (IS_NUMBER_CHECK) {
				combinedStrings += numberString;
			} 

			LENG = combinedStrings.Length;
			for(int i = 0; i<WORD_COUNT; i++){
				next = random.Next(combinedStrings.Length);
				char[] array = combinedStrings.ToCharArray();
				char next_char = array[next];

					if (checkIfDictionaryContains(next_char)) {
						int other = random.Next ((int)(next_char/getIntValue(next)));
					    Console.Write(next_char);
						char to_add = combinedStrings[other];
						WORD_DIC.AddLast(to_add);
						result = result + to_add.ToString();
					} else {
						WORD_DIC.AddLast(next_char);
						result = result + next_char.ToString();
				}
				

			}
			return result;
		}

		protected int getIntValue(int rand){
			int result = 0;
			if (rand < LENG|rand == LENG) {
				int min = LENG - rand;
				int half = LENG / 2;

				if (min > half) {
					result = random.Next(half,min);
				} else {
					result = random.Next(min, LENG);
				}
			} else {
				result = rand / LENG;
			
			}
			Console.Write(result+"\n");
			return result;
		}

		protected Boolean checkIfDictionaryContains(char character){
			Boolean if_contains = false;

			if(WORD_DIC.Contains(character)){
				if_contains = true;
			}
			return if_contains;
		}
	}
}

