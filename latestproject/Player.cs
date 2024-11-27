using FFImageLoading.Maui;
using Microsoft.Maui.Platform;

namespace latestproject;

public delegate void Callback();

public class Player:Animacao
{
    public Player (CachedImageView a) : base (a)
    {
        for (int i = 1; i < 9; ++i)
            Animacao1.Add($"lobo{i.ToString("D2")}.png");
        for (int i = 1; i < 4; ++i)
            Animacao2.Add($"lobomorre{i.ToString("D2")}.png");
        SetAnimacaoAtiva(1);    
    }

    public void Die()
    {
        loop = false;
        SetAnimacaoAtiva(2);
    }


    public void Run()
    {
        loop=true;
        SetAnimacaoAtiva(1);
        Corre();
    }
    
    public void MoveY(int s)
    {
        ImageView.TranslationY +=s;
    }

    public double GetY()
    {
        return ImageView.TranslationY;
    }
    
    public void SetY(double a)
    {
        ImageView.TranslationY = a;
    }

}