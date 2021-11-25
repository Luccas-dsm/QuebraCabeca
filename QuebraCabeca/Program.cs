using System;
using System.Collections.Generic;
using System.Linq;

namespace QuebraCabeca
{

    class Quebra
    {
        //MESA
        int[,] mesaEmbaralhada;
        int[,] mesaMontagem;
        // vetor aux;
        int[] vetAux;
        //PEÇAS
        int numPecas;

        //numCasas
        int numCasas;
        //lista de numeros aleatorios
        List<int> aleatorio = new List<int>();

        //PREENCHE A LISTA DE NUMEROS ALEATORIOS UNICOS
        void addLista(int tam)
        {
            Random random = new Random(tam + 1);
            int valor = random.Next(1, tam + 1);
            int i = 0;
            while (i < tam)
            {
                if (!aleatorio.Contains(valor))
                {
                    aleatorio.Add(valor);
                    i++;
                }
                else
                    valor = random.Next(1, tam + 1);
            }
        }
        //RETORNA O ULTIMO VALOR DA LISTA E REMOVE PARA NAO REPETIR
        int removeLista()
        {
            int numero = 0;
            foreach (int n in aleatorio)
            {
                numero = n;
            }
            aleatorio.Remove(numero);
            return numero;
        }
        public Quebra(int n) // recebe o numero de pecas
        {
            numPecas = n;
            numCasas = n * n;
            vetAux = new int[numCasas];
            mesaEmbaralhada = new int[numPecas, numPecas];
            mesaMontagem = new int[numPecas, numPecas];
        }

        public void preencher() //PREENCHE O NOSSO QUEBRA CABECA PARA FAZERMOS OS TESTES
        {
            addLista(numCasas);
            for (int i = 0; i <= numPecas - 1; i++)
            {
                for (int j = 0; j <= numPecas - 1; j++)
                {
                    mesaEmbaralhada[i, j] = removeLista();
                    Console.Write(mesaEmbaralhada[i, j] + "\t");
                }
                Console.WriteLine();
            }
        }

        public void imprime()
        {
            for (int i = 0; i <= numPecas - 1; i++)
            {
                for (int j = 0; j <= numPecas - 1; j++)
                {
                    Console.Write(mesaMontagem[i, j] + "\t");
                }
                Console.WriteLine();
            }
        }

        //verifico se ja existe uma peça naquela posicao
        bool aceitavel(int x, int y)
        {
            bool resultado = x > 0 && x <= numPecas - 1;
            resultado = resultado && y > 0 && y <= numPecas - 1;
            resultado = resultado && mesaEmbaralhada[x, y] != 0;//conferir
            return resultado;
        }

        public void ordenaMatriz(int x, int y, int n)
        {
            if (vetAux[vetAux.Length - 1] == 0)
            {
                preencheVetor(0, 0, 0);
                bubleSort(vetAux);
            }
            if (x * numPecas + y < numCasas)
            {
                mesaMontagem[x, y] = vetAux[n];
                y++;
                if (y == numPecas)
                {
                    y = 0;
                    x++;
                }
                ordenaMatriz(x, y, n + 1);
            }
        }

        public void preencheVetor(int x, int y, int n)
        {
            if (x * numPecas + y < numCasas)
            {
                vetAux[n] = mesaEmbaralhada[x, y];
                y++;
                if (y == numPecas)
                {
                    y = 0;
                    x++;
                }
                preencheVetor(x, y, n + 1);
            }
        }
        //BUBLESORT ORDENA VETOR
        bool maiorQue(int vetor, int w) { return (vetor > w); }

        void trocar(int p1, int p2, int[] dados)
        {
            int trocaAux = dados[p1];
            dados[p1] = dados[p2];
            dados[p2] = trocaAux;
        }

        void bubleSort(int[] dados)
        {

            for (int referencia = dados.Length - 1; referencia > 0; referencia--)
            {
                int trocas = 0;
                for (int aux = 0; aux < referencia; aux++)
                {

                    if (maiorQue(dados[aux], dados[aux + 1]))
                    {
                        trocar(aux, aux + 1, dados);
                        trocas++;
                    }

                }
                if (trocas == 0)
                    return;
            }
        }


        class Program
        {
            static void Main(string[] args)
            {
                Quebra quebraCabeca = new Quebra(2);
                Console.WriteLine("Quebra cabeca desordenado\n");
                quebraCabeca.preencher();
                Console.WriteLine("\nQuebra cabeca ordenado\n");
                quebraCabeca.ordenaMatriz(0, 0, 0);
                quebraCabeca.imprime();
                //  quebraCabeca.matriz(0,0);
                Console.ReadKey();
            }
        }
    }
}
