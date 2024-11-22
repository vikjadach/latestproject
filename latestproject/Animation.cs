using FFImageLoading.Maui;
namespace Dino
{
public class Animation
{
    protected List<String> Animacao1 = new List<String>();

    protected List<String> Animacao2 = new List<String>();

    protected List<String> Animacao3 = new List<String>();

    protected bool loop = true;

    protected int AnimacaoAtiva = 1;

    protected CachedImageView ImageView;


//----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    private bool  Parado = true;

    private int FrameAtual = 1;
//----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    

    public Animation (CachedImageView a)
    {
        ImageView = a
        ;
    }

//----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
 
    public void Para()
    {
        Parado = true;
    }

    public void Play()
    {
        Parado = false;
    }

    public void SetAnimacaoAtiva(int a)
    {
        AnimacaoAtiva = a;
    }

//----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    public void Desenha()
    {
        if(Parado)
            return;
        
        string NomeDoArquivo = "";
        int AnimationHeight = 0;

        if (AnimacaoAtiva == 1)
        {
            NomeDoArquivo = Animacao1[FrameAtual];
            AnimationHeight = Animacao1.Count;
        }
        else if (AnimacaoAtiva == 2)
        {
            NomeDoArquivo = Animacao2[FrameAtual];
            AnimationHeight = Animacao2.Count;            
        }
        else if (AnimacaoAtiva == 3)
        {
            NomeDoArquivo = Animacao3[FrameAtual];
            AnimationHeight = Animacao3.Count;            
        }

        ImageView.Source = ImageSource.FromFile(NomeDoArquivo);
        FrameAtual ++;

        if (FrameAtual >= AnimationHeight)
        {
            if(loop)
            {
                FrameAtual = 0;
            }
            else
            {
                Parado = true;
                QuandoParar();
            }
        }
    }
    
    public virtual void QuandoParar()
    {

    }
//----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
 


}

}