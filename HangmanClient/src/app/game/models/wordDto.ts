export interface WordDto {
  wordId?: string;
  word?: string;
  theme?: string;
  remainingLetters?: string;
  sendChar?: string;
  isWin: boolean;
  hasChar: boolean;
}
