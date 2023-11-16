# RootSearch
1. WORDS.txt - предоставленный словарь БРуМС, используемый для парсинга в собственные структуры.
2. Файлы ресурсов хранятся в папке resource. Списки в файлах обязательно должны быть отсортированы в алфавитном порядке по первому столбцу.
   Root.xls имеет 2 столбца - соответствие фактического корня и его транскрипции (их может быть много).
   Suffix.xls, Prefix.xls, Proclitic.xls - имеют по 1 столбцу.
   (-)
4. Word.cs содержит урезанную структуру слова, необходимую для парсинга. 
   Также содержит методы:
    override public string ToString()
    public string ToStringRoot() выводит только нужные по заданию поля
    public bool IsClassifiedPreffixes(string[] pref) является входной набор приставок классификатором данного слова    
    public bool IsClassifiedSuffixes(string[] suf) является входной набор суффиксов классификатором данного слова
    public bool IsClassified(string[] pref, string[] suf) является ли входной набор аффиксов классификатором данного слова
    а туть ещё компоратор!!!
5. Parser.cs
   private string[] ParseWordPrefix(string word, out string remainder, out string root) 
    Получает слово, выходными параметрами являются остаток слова и корень при необхобимости. Обрезает приставки и возвращает иж в виде массива строк. 
   private string[] ParseWordSuffix(string word, out string remainder, out string root)
    Аналогично для суффиксов
   private Word ParsePartWord(string word, string fullWord, string transcripton)
    Получает строку (слово с одним корнем), полное слово и его транскрипцию. Возвращает слово, имеющее 1 корень, в виде структуры Word.
   private Word ParseStringIntoWords(string word, out string secondRootWord, ref string fullWord, ref string transcription)
    Получает 
    Парсит всю строку, используя заданные разделители.
    Возвращает Word на каждом шаге (внутри рекурсия).
   отправка set/list - что делать с ним дальше печатать ли парсеру - что не хорошо) лучше отправить коллекцию наверх, для распечатки
6. 
7. 
8. Form1 что сделать при загрузке формы.

ДОБАВИТЬ ОПИСАНИЕ ВСЕХ МОДУЛЕЙ!
