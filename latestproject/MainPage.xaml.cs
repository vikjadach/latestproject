using FFImageLoading.Maui;
namespace Dino;

public partial class MainPage : ContentPage
{
//____________________________________________________________________________________________________________________________________	

	bool EstaMorto = false;
	bool pulo = false;
    bool EstaNoChao = true;
	bool EstaNoAr = false;
	bool EstaPulando = false;
//____________________________________________________________________________________________________________________________________	
	int velocidade1 = 0;
	int velocidade2 = 0;
	int velocidade3 = 0;
	int velocidade = 0;
	int LarguraJanela = 0;
	int AlturaJanela = 0;
//-------------------
	Player player;
//____________________________________________________________________________________________________________________________________
	int TempoPulando = 0;
	int TempoNoAr = 0;
	const int ForcaPulo = 12;
	const int maxTempoPulando = 10;
	const int maxTempoNoAr = 4;
	const int ForcaGravidade = 6;
	const int TempoEntreFrames = 29;

//____________________________________________________________________________________________________________________________________

	public MainPage()
	{
		InitializeComponent();
		player = new Player(persona);
		player.Run();
	}
	protected override void OnSizeAllocated(double width, double height)
	{
		base.OnSizeAllocated(width, height);
		CorrigeTamanhoCenario(width, height);
		CalculaVelocidade(width);
	}
	void CalculaVelocidade(double width)
	{
		velocidade1 = (int)(width * 0.001);
		velocidade2 = (int)(width * 0.004);
		velocidade3 = (int)(width * 0.008);
		velocidade = (int)(width * 0.01);
	}
	void CorrigeTamanhoCenario(double width, double height)
	{
		foreach (var ceu in HsLayer1.Children)
			(ceu as Image).WidthRequest = width;
		foreach (var predio in HsLayer2.Children)
			(predio as Image).WidthRequest = width;
		foreach (var mato in HsLayer3.Children)
			(mato as Image).WidthRequest = width;
		foreach (var chao in HsLayer4Chao.Children)
			(chao as Image).WidthRequest = width;

		HsLayer1.WidthRequest = width;
		HsLayer2.WidthRequest = width;
		HsLayer3.WidthRequest = width;
		HsLayer4Chao.WidthRequest = width;
	}

	void GerenciaCenarios()
	{
		MoveCenarios();
		GerenciaCenarios(HsLayer1);
		GerenciaCenarios(HsLayer2);
		GerenciaCenarios(HsLayer3);
		GerenciaCenarios(HsLayer4Chao);
	}

	void MoveCenarios()
	{
		HsLayer1.TranslationX -= velocidade1;
		HsLayer2.TranslationX -= velocidade2;
		HsLayer3.TranslationX -= velocidade3;
		HsLayer4Chao.TranslationX -= velocidade;
	}
	void GerenciaCenarios(HorizontalStackLayout HSL)
	{
		var view = (HSL.Children.First() as Image);
		if (view.WidthRequest + HSL.TranslationX < 0)
		{
			HSL.Children.Remove(view);
			HSL.Children.Add(view);
			HSL.TranslationX = view.TranslationX;
		}
	}
async Task Desenha()
	{ 
		while (!EstaMorto)
		{
			     GerenciaCenarios();
			if (!EstaPulando && !EstaNoAr)
			{
				AplicaGravidade();
				player.Desenha();
			}
			else
				AplicaPulo();
			await Task.Delay(TempoEntreFrames);
		}
	}
	protected override void OnAppearing()
	{
		base.OnAppearing();
		Desenha();
	}
	void AplicaPulo()
	{
		EstaNoChao = false;
		if (EstaPulando && TempoPulando >= maxTempoPulando)
		{
			EstaPulando = false;
			EstaNoAr = true;
			TempoNoAr = 0;
		}
		else if (EstaNoAr && TempoNoAr >= maxTempoNoAr)
		{
			EstaPulando = false;
			EstaNoAr = false;
			TempoPulando = 0;
			TempoNoAr = 0;
		}
		else if (EstaPulando && TempoPulando < maxTempoPulando)
		{
			player.MoveY(-ForcaPulo);
			TempoPulando++;
		}
		else if (EstaNoAr)
			TempoNoAr++;
	}
	void OnGridTapped(object o, TappedEventArgs a)
	{
		if (EstaNoChao)
			EstaPulando = true;
	}
	void AplicaGravidade()
	{
		if (player.GetY() < 0)
			player.MoveY(ForcaGravidade);
		else if (player.GetY() >= 0)
		{
			player.SetY(0);
			EstaNoChao = true;
		}
	}
}