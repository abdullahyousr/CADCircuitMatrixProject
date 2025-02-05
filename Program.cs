    Console.WriteLine();

    // int Nodes = Convert.ToInt32(Console.ReadLine());

    // int Branches = Convert.ToInt32(Console.ReadLine());

    // CircuitMatrices cirMat =  new CircuitMatrices(Nodes,Branches);

    // cirMat.CreateIncidenceMat();
    // cirMat.CreateCut_SetMat();
    // cirMat.CreateTie_SetMat();

    double [][] incidMat = new double [][]
    {
        new double []{1,1,-1,0},
        new double []{0,-1,0,1},
        new double []{-1,0,1,-1},
    };
    
    CircuitMatrices cirMat =  new CircuitMatrices(incidMat);

    cirMat.PrintIncidenceMat();
    cirMat.PrintA_TreeMat();
    cirMat.PrintA_LinkMat();

    cirMat.PrintCut_SetMat();
    cirMat.PrintC_TreeMat();
    cirMat.PrintC_LinkMat();
    Console.WriteLine("------------------------------------------");
    
    Console.Write("Rows:");
    int lenRows = cirMat.C.Length;
    Console.WriteLine(lenRows);
    
    Console.Write("Columns:");
    int lenCol =cirMat.C[0].Length;
    Console.WriteLine(lenCol);

    cirMat.PrintTie_SetMat();
    cirMat.PrintB_TreeMat();
    cirMat.PrintB_LinkMat();

    double[][] Z_Bran = new double[4][];
    Z_Bran[0] = new double[] { 5, 0,  0, 0 };
    Z_Bran[1] = new double[] { 0, 10, 0, 0 };
    Z_Bran[2] = new double[] { 0, 0,  5, 0 };
    Z_Bran[3] = new double[] { 0, 0,  0, 5 };

    double [][] E_Sour = new double [][]
    {
        new double []{0},
        new double []{0},
        new double []{10},
        new double []{0}
    };
    
    double [][] I_Sour = new double [][]
    {
        new double []{0},
        new double []{0},
        new double []{0},
        new double []{0}
    };
    cirMat.CreateZ_Bran(Z_Bran);
    cirMat.CreateE_Source(E_Sour);
    cirMat.CreateI_Source(I_Sour);

    cirMat.PrintZ_Bran();
    cirMat.PrintE_Source();
    cirMat.PrintI_Source();

    cirMat.CreateI_Line();  
    cirMat.CreateJ_Bran();
    cirMat.CreateV_Bran();

    cirMat.PrintI_Line();
    cirMat.PrintJ_Bran();
    cirMat.PrintV_Bran();

    cirMat.FileIncidenceMat();
    cirMat.Writer.Close();