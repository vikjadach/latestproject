using Microsoft.UI.Xaml.Controls;

namespace latestproject;

public partial class MainPage : ContentPage
{
	

	bool estamorto = false;
	bool estaPulando = false;
	const int tempoEntreFrame = 25;
	int velocidade1 = 0;
	int velocidade2 = 0;
	int velocidade3 = 0;
	int velecidade = 0;
	int alturadajanela = 0;
	int larguradajanela = 0;


	public MainPage()
	{
		InitializeComponent();
		player = new Player(lobo);
		player.Run();
	}


	protected override void OnSizeAllocated(double w, double h)
	{
		base.OnSizeAllocated(w, h);
		CorrigeTamanhoCenario(w, h);
		CalculaVelocidade(w);
	}
	void CalculaVelocidade(double w)
	{
		velocidade1 = (int)(w * 0.001);
		velocidade2 = (int)(w * 0.004);
		velocidade3 = (int)(w * 0.008);
		velocidade = (int)(w * 0.001);
	}

	void CorrigeTamanhoCenario(double w, double h)
	{
		foreach (var A in primeiraimg.Children)
			(A as Image).WidthRequest = w;
		foreach (var A in segundaimg.Children)
			(A as Image).WidthRequest = w;
		foreach (var A in terceiraimg.Children)
			(A as Image).WidthRequest = w;
		foreach (var A in quartaimg.Children)
			(A as Image).WidthRequest = w;

		primeiraimg.WidthRequest = w * 1.5;
		segundaimg.WidthRequest = w * 1.5;
		terceiraimg.WidthRequest = w * 1.5;
		quartaimg.WidthRequest = w * 1.5;;
	}
	void GerenciaCenarios()
	{
		MoveCenario();
		GerenciaCenarios(primeiraimg);
		GerenciaCenarios(segundaimg);
		GerenciaCenarios(terceiraimg);
		GerenciaCenarios(quartaimg);

	}
	void MoveCenario()
	{
		primeiraimg.TranslationX -= velocidade1;
		segundaimg.TranslationX -= velocidade2;
		terceiraimg.TranslationX -=velocidade3;
		quartaimg.TranslationX -= velocidade;
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
		while (!estamorto)
		{
			GerenciaCenarios();
			await Task.Delay(tempoEntreFrame);
			Player.Desenha;
		}
	}
    protected override void OnAppearing()
    {
        base.OnAppearing();
		Desenha();
    }

}