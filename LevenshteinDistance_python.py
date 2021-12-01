def iterative_levenshtein(s, t):
    """
         iterative_levenshtein(s, t) -> ldist
         ldist, dizeler arasındaki Levenshtein mesafesidir
         s ve t.
         Tüm i ve j için, dist[i,j] Levenshtein'i içerecektir
         s'nin ilk i karakterleri ile
         t'nin ilk j karakteri
     """

    rows = len(s)+1
    cols = len(t)+1
    dist = [[0 for x in range(cols)] for x in range(rows)]

   # kaynak önekleri boş dizelere dönüştürülebilir
   # silme ile:
    for i in range(1, rows):
        dist[i][0] = i


    # karakterleri ekleyerek boş bir kaynak dizeden hedef önek oluşturulabilir
    
    for i in range(1, cols):
        dist[0][i] = i
        
    for col in range(1, cols):
        for row in range(1, rows):
            if s[row-1] == t[col-1]:
                cost = 0
            else:
                cost = 1
            dist[row][col] = min(dist[row-1][col] + 1,      # silme
                                 dist[row][col-1] + 1,      # ilave
                                 dist[row-1][col-1] + cost) # yer değiştirme

    for r in range(rows):
        print(dist[r])
    
 
    return dist[row][col]

print(iterative_levenshtein("sena", "SEna"))