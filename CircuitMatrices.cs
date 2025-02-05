class CircuitMatrices
{
    private int Nodes { get; set; }
    private int Branches { get; set; }
    private int treeBranches { get; set; }
    private int treeLinks { get; set; }
    private int AReducedRows { get; set; }
    StreamWriter writer = new StreamWriter("Printed_Matrices.txt");

    public double[][] A { get; set; }
    public double[][] A_Tree { get; set ; }
    public double[][] A_Link { get; set; }
    public double[][] B { get; set; }
    public double[][] B_Tree { get; set; }
    public double[][] B_Link { get; set; }
    public double[][] C { get; set; }
    public double[][] C_Tree { get; set; }
    public double[][] C_Link { get; set; }
    public double[][] A_Reduced { get; set; }
    public double[][] Z_Bran { get; set; }
    public double[][] I_Source { get; set; }
    public double[][] E_Source { get; set; }
    public double[][] I_Line { get; set; }
    public double[][] J_Bran { get; set; }
    public double[][] V_Bran { get; set; }
    public StreamWriter Writer { get => writer; set => writer = value; }

    
//------------------------------------------------------------------------
    
    // Constructors
    public CircuitMatrices (double [][] incidMat)
    {
        Nodes = incidMat.Length;
        Branches = incidMat[0].Length;
        treeBranches = Nodes - 1;
        treeLinks = Branches - treeBranches;
        AReducedRows = Nodes - 1;
        CreateIncidenceMat(incidMat);
        CreateCut_SetMat();
        CreateTie_SetMat();
    }

    public CircuitMatrices (int Nod, int Bran)
    {
        Nodes = Nod;
        Branches = Bran;
        treeBranches = Nodes - 1;
        treeLinks = Branches - treeBranches;
        AReducedRows = Nodes - 1;
    }

//------------------------------------------------------------------------
    
    // Incidence matrix
    public void CreateIncidenceMat()
    {
        A = new double[Nodes][];
        for(int i=0; i<Nodes; i++)
        {
            A[i] = new double[Branches];
        }
        //Creating incidence matrix
        for(int j=0; j<Nodes; j++)
        {
            for(int i=0; i<Branches; i++)
            {
                A[j][i] = Convert.ToInt32(Console.ReadLine());
            }
        }
        CreateIncidenceReducedMat();
        CreateA_TreeMat();
        CreateA_LinkMat();
    }
    public void CreateIncidenceMat(double [][]matr)
    {
        A = matr;
        CreateIncidenceReducedMat();
        CreateA_TreeMat();
        CreateA_LinkMat();
    }
    public void CreateIncidenceReducedMat(){
        A_Reduced = new double[AReducedRows][];
        for(int i=0; i<AReducedRows; i++)
        {
            A_Reduced[i] = new double[Branches];
        }
        //Creating incidence matrix
        for(int j=0; j<AReducedRows; j++)
        {
            for(int i=0; i<Branches; i++)
            {
                A_Reduced[j][i] = A[j][i];
            }
        }
    }
    public void PrintIncidenceMat()
    {
        //Printing incidence matrix
        Console.WriteLine("Incidence Matrix:");
        for(int j=0; j<Nodes; j++)
        {
            Console.Write("{");
            for(int i=0; i<Branches; i++)
            {
                Console.Write($"\t{A[j][i]}\t");
            }
            Console.Write("}");
            Console.WriteLine();
        }
        Console.WriteLine();
    }
    public void FileIncidenceMat()
    {
        Writer.WriteLine("Incidence Matrix:");
        for(int j=0; j<Nodes; j++)
        {
            Writer.Write("{");
            for(int i=0; i<Branches; i++)
            {
                Writer.Write($"\t{A[j][i]}\t");
            }
            Writer.Write("}");
            Writer.WriteLine();
        }
        Writer.WriteLine();
    }

//------------------------------------------------------------------------
    
    // A Tree matrix   
    public void CreateA_TreeMat()
    {
        A_Tree = new double[AReducedRows][];
        for(int i=0; i<AReducedRows; i++)
        {
            A_Tree[i] = new double[treeBranches];
        }
        //Creating A_Tree matrix
        for(int j=0; j<AReducedRows; j++)
        {
            for(int i= 0; i < treeBranches; i++)
            {
                A_Tree[j][i] = A[j][i];
            }
        }
    }
    public void PrintA_TreeMat()
    {
        Console.WriteLine("Incidence Tree Matrix:");
        for(int j=0; j<AReducedRows; j++)
        {
            Console.Write("{");
            for(int i=0; i<treeBranches; i++)
            {
                Console.Write($"\t{A_Tree[j][i]}\t");
            }
            Console.Write("}");
            Console.WriteLine();
        }
        Console.WriteLine();
        //------------//
    }

//------------------------------------------------------------------------
    
    // Link matrix
    public void CreateA_LinkMat()
    {
        // //A_Link matrix
        A_Link = new double[AReducedRows][];
        for(int i=0; i<AReducedRows; i++)
        {
            A_Link[i] = new double[treeLinks];
        }
        //Creating A_Link matrix
        int k;
        for(int j=0; j<AReducedRows; j++)
        {
            k=0;
            for(int i=treeBranches; i<Branches; i++)
            {
                A_Link[j][k] = A[j][i];
                k++;
            }
        }
    }
    public void PrintA_LinkMat()
    {
        Console.WriteLine("Incidence Link Matrix:");
        for(int j=0; j<AReducedRows; j++)
        {
            Console.Write("{");
            for(int i=0; i<treeLinks; i++)
            {
                Console.Write($"\t{A_Link[j][i]}\t");
            }
            Console.Write("}");
            Console.WriteLine();
        }
        Console.WriteLine();
    }

//------------------------------------------------------------------------
    
    // Tie_Set matrix
    public void CreateTie_SetMat()
    {
        CreateB_TreeMat();
        CreateB_LinkMat();
        B  = new double [B_Tree.Length][];
        for(int i=0; i<B_Tree.Length; i++)
        {
            B[i] = new double[Branches];
        }
        int k;
        for(int j=0; j<B_Tree.Length; j++)
        {
            for(int i= 0; i < treeBranches; i++)
            {
                B[j][i] = B_Tree[j][i];
            }
            k=0;
            for(int i=treeBranches; i<Branches; i++)
            {
                B[j][i] = B_Link[j][k];
                k++;
            }
        }
    }
    public void PrintTie_SetMat()
    {
        Console.WriteLine("Tie_Set Matrix:");
        for(int j=0; j<B.Length; j++)
        {
            
            Console.Write("{");
            for(int i=0; i<Branches; i++)
            {
                Console.Write($"\t{B[j][i]}\t");
            }
            Console.Write("}");
            Console.WriteLine();
        }
            Console.WriteLine();
    }

//------------------------------------------------------------------------

    // B Tree matrix   
    public void CreateB_TreeMat()
    {   
        B_Tree = MatrixOperations.MatTranspose(C_Link);
        B_Tree = MatrixOperations.MatrixMinus(B_Tree);
    }
    public void PrintB_TreeMat()
    {
        Console.WriteLine("Tie_Set Tree Matrix:");
        for(int j=0; j<B_Tree.Length; j++)
        {
            Console.Write("{");
            for(int i=0; i<treeBranches; i++)
            {
                Console.Write($"\t{B_Tree[j][i]}\t");
            }
            Console.Write("}");
            Console.WriteLine();
        }
            Console.WriteLine();
        //------------//
    }

//------------------------------------------------------------------------

    // B Link matrix     
    public void CreateB_LinkMat()
    {
        // //B_Link matrix
        B_Link = new double[B_Tree.Length][];
            for(int i=0; i<B_Tree.Length; i++)
            {
                B_Link[i] = new double[treeLinks];
            }
        //Creating B_Link matrix
        B_Link = MatrixOperations.MatrixIdentity(B_Tree.Length);
    }
    public void PrintB_LinkMat()
    {
        Console.WriteLine("Tie_Set Link Matrix:");
        for(int j=0; j<B_Tree.Length; j++)
        {
            Console.Write("{");
            for(int i=0; i<treeLinks; i++)
            {
                Console.Write($"\t{B_Link[j][i]}\t");
            }
            Console.Write("}");
            Console.WriteLine();
        }
            Console.WriteLine();
    }
//----------------------------------------------------------------------------

    // Cut Set matrix  
    public void CreateCut_SetMat()
    {
        C  = new double [AReducedRows][];
        for(int i=0; i<AReducedRows; i++)
        {
            C[i] = new double[Branches];
        }
        double [][] A_TreeInve = MatrixOperations.MatInverse(A_Tree);
        C = MatrixOperations.MatProduct(A_TreeInve,A_Reduced);
        CreateC_TreeMat();
        CreateC_LinkMat();
    }
    public void PrintCut_SetMat()
    {
        Console.WriteLine("Cut_Set Matrix:");
        for(int j=0; j<AReducedRows; j++)
        {
            Console.Write("{");
            for(int i=0; i<Branches; i++)
            {
                Console.Write($"\t{C[j][i]}\t");
            }
            Console.Write("}");
            Console.WriteLine();
        }
            Console.WriteLine();
        //------------//
    }

//------------------------------------------------------------------------

    // C Tree matrix     
    public void CreateC_TreeMat()
    {
        C_Tree = MatrixOperations.MatrixIdentity(AReducedRows);
    }
    public void PrintC_TreeMat()
    {
        Console.WriteLine("Cut_Set Tree Matrix:");
        for(int j=0; j<AReducedRows; j++)
        {
            Console.Write("{");
            for(int i=0; i<treeBranches; i++)
            {
                Console.Write($"\t{C_Tree[j][i]}\t");
            }
            Console.Write("}");
            Console.WriteLine();
        }
            Console.WriteLine();
        //------------//
    }
//------------------------------------------------------------------------

    // C Link matrix    
    public void CreateC_LinkMat()
    {
        // //C_Link matrix
        C_Link = new double[AReducedRows][];
            for(int i=0; i<AReducedRows; i++)
            {
                C_Link[i] = new double[treeLinks];
            }
        //Creating C_Link matrix
        int k;
        for(int j=0; j<AReducedRows; j++)
        {
            k=0;
            for(int i=treeBranches; i<Branches; i++)
            {
                C_Link[j][k] = C[j][i];
                k++;
            }
        }
    }
    public void PrintC_LinkMat()
    {
        Console.WriteLine("Cut_Set Link Matrix:");
        for(int j=0; j<AReducedRows; j++)
        {
            Console.Write("{");
            for(int i=0; i<treeLinks; i++)
            {
                Console.Write($"\t{C_Link[j][i]}\t");
            }
            Console.Write("}");
            Console.WriteLine();
        }
            Console.WriteLine();
        //------------//
    }

//------------------------------------------------------------------------

    // Z Branch  
    public void CreateZ_Bran(double [][]ZMat){
        Z_Bran = ZMat;
    }
    public void PrintZ_Bran()
    {
        Console.WriteLine("Impedence Branch:");
        for(int j=0; j<Z_Bran.Length; j++)
        {
            Console.Write("{");
            for(int i=0; i<Z_Bran[0].Length; i++)
            {
                Console.Write($"\t{Z_Bran[j][i]}\t");
            }
            Console.Write("}");
            Console.WriteLine();
        }
            Console.WriteLine();
        //------------//
    }

//------------------------------------------------------------------------
    // E Source
    public void CreateE_Source(double [][]EMat){
        E_Source = EMat;
    }
    public void PrintE_Source()
    {
        Console.WriteLine("Voltage Source:");
        for(int j=0; j<E_Source.Length; j++)
        {
            Console.Write("{");
            for(int i=0; i<E_Source[0].Length; i++)
            {
                Console.Write($"\t{E_Source[j][i]}\t");
            }
            Console.Write("}");
            Console.WriteLine();
        }
            Console.WriteLine();
        //------------//
    }

//------------------------------------------------------------------------
    // I Source
    public void CreateI_Source(double [][]IMat){
        I_Source = IMat;
    }
    public void PrintI_Source()
    {
        Console.WriteLine("Current Source:");
        for(int j=0; j<I_Source.Length; j++)
        {
            Console.Write("{");
            for(int i=0; i<I_Source[0].Length; i++)
            {
                Console.Write($"\t{I_Source[j][i]}\t");
            }
            Console.Write("}");
            Console.WriteLine();
        }
            Console.WriteLine();
        //------------//
    }

//------------------------------------------------------------------------
    // I Line
    public void CreateI_Line()
    {
        double [][] BTrans = MatrixOperations.MatTranspose(B);
        double [][] Prod_ZBTrans   = MatrixOperations.MatProduct(Z_Bran,BTrans);
        double [][] Prod_BZBTrans   = MatrixOperations.MatProduct(B,Prod_ZBTrans);

        double [][] Prod_BESour   = MatrixOperations.MatProduct(B,E_Source);

        double [][] inver = MatrixOperations.MatInverse(Prod_BZBTrans);
        double [][] Prod_ZISour   = MatrixOperations.MatProduct(Z_Bran,I_Source);
        double [][] Prod_BZISour   = MatrixOperations.MatProduct(B,Prod_ZISour);
        double [][] Sub = MatrixOperations.Subtract(Prod_BESour,Prod_BZISour);

        I_Line  = MatrixOperations.MatProduct(inver,Sub);
    }
    public void PrintI_Line()
    {
        Console.WriteLine("Current Line:");
        for(int j=0; j<I_Line .Length; j++)
        {
            Console.Write("{");
            for(int i=0; i<I_Line [0].Length; i++)
            {
                Console.Write($"\t{I_Line [j][i]}\t");
            }
            Console.Write("}");
            Console.WriteLine();
        }
        Console.WriteLine();
        //------------//
    }

//------------------------------------------------------------------------
    // J Branch
    public void CreateJ_Bran()
    {
        double [][] BTrans = MatrixOperations.MatTranspose(B);
        J_Bran = MatrixOperations.MatProduct(BTrans,I_Line );
    }
    public void PrintJ_Bran()
    {
        Console.WriteLine("Current Branch:");
        for(int j=0; j<J_Bran.Length; j++)
        {
            Console.Write("{");
            for(int i=0; i<J_Bran[0].Length; i++)
            {
                Console.Write($"\t{J_Bran[j][i]}\t");
            }
            Console.Write("}");
            Console.WriteLine();
        }
        Console.WriteLine();
        //------------//
    }

//------------------------------------------------------------------------
    // V Branch
    public void CreateV_Bran()
    {
        double [][] addJISour = MatrixOperations.Add(J_Bran,I_Source);
        double [][] prod_ZJISour = MatrixOperations.MatProduct(Z_Bran,addJISour);
        V_Bran = MatrixOperations.Subtract(prod_ZJISour,E_Source);
    }
    public void PrintV_Bran()
    {
        Console.WriteLine("Voltage Branch:");
        for(int j=0; j<V_Bran.Length; j++)
        {
            Console.Write("{");
            for(int i=0; i<V_Bran[0].Length; i++)
            {
                Console.Write($"\t{V_Bran[j][i]}\t");
            }
            Console.Write("}");
            Console.WriteLine();
        }
        Console.WriteLine();
        //------------//
    }
}