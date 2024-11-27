using Microsoft.Maui.Controls.PlatformConfiguration.AndroidSpecific;

namespace latestproject;

public partial class MainPage : ContentPage
{
	//Player player;
	Inimigos inimigos;

	public MainPage()
	{
		InitializeComponent();
		//player = new Player(lobo);
		//player.Run();
	}

		bool estamorto = false;
		bool estaPulando = false;
		const int temporEntreFrames = 25;
		int velocidade1 = 0;
		int velocidade2 = 0;
		int velocidade3 = 0;
		int velocidade = 0;
		int larguraJanela = 0;
		int alturaJanela = 0;
		const int forcaGravidade = 6;
		bool estaNoChao = true;
		bool estaNoAr = false;
		int tempoPulando = 0;
		int tempoNoAr = 0;
		const int forcaPulo = 8;
		const int maxTempoPulando = 6;
		const int maxTempoAr = 4;
		

	void AplicaPulo()
	{
	estaNoChao=false;
	if(estaPulando && tempoPulando >= maxTempoPulando)
	{
		estaPulando=false;
		estaNoAr=true;
		tempoNoAr=0;
	}
	else if( estaNoAr && tempoNoAr >= maxTempoAr)
	{
		estaPulando=false;
		estaNoAr=false;
		tempoPulando=0;
		tempoNoAr=0;
	}
	else if(estaPulando && tempoPulando < maxTempoPulando)
	{
		player.MoveY (-forcaPulo);
		tempoPulando++;
	}
	else if (estaNoAr)
			tempoNoAr++;
	}
		void OnGridTapped(object o, TappedEventArgs a)
		{
			if (estaNoChao)
				estaPulando = true;
		}



	void AplicaGravidade()
	{
		if
		(player.GetY()<0)
			player.MoveY(forcaGravidade);

			else if
			(player.GetY() >= 0)
			
				player.SetY(0);
				estaNoChao = true;
			
	}
	protected override void OnSizeAllocated(double  w, double h)
	{
		base.OnSizeAllocated(w, h);
		CorrigeTamanhoCenario(w, h);
		CalculaVelocidade(w);
		inimigos = new Inimigos(-w);
		inimigos.Add(new Inimigo(inimigo1));
		inimigos.Add(new Inimigo(inimigo2));
		inimigos.Add(new Inimigo(inimigo3));
		inimigos.Add(new Inimigo(inimigo4));
	}

	void CalculaVelocidade(double w)
	{
		velocidade1 = (int)(w * 0.001);
		velocidade2 = (int)(w * 0.004);
		velocidade3 = (int)(w * 0.008);
		velocidade 	= (int)(w * 0.01);
	}

	void CorrigeTamanhoCenario(double w, double h)
	{
		foreach (var A in primeiraimg.Children)
			(A as Image).WidthRequest = w;
		
		foreach (var A in segundaimg.Children)
			(A as Image).WidthRequest = w;

		foreach (var A in terceiraimg.Children)
			(A as Image).WidthRequest = w;
		
		foreach(var A in quartaimg.Children)
			(A as Image).WidthRequest = w;

		primeiraimg.WidthRequest = w;
		segundaimg.WidthRequest = w;
		terceiraimg.WidthRequest = w;
		quartaimg.WidthRequest = w; 
	}

	void GerenciaCenario()
	{
		MoveCenario();
		GerenciaCenario(primeiraimg);
		GerenciaCenario(segundaimg);
		GerenciaCenario(terceiraimg);
		GerenciaCenario(quartaimg);
	}

	void MoveCenario()
	{
		primeiraimg.TranslationX -= velocidade1;
		segundaimg.TranslationX -= velocidade2;
		terceiraimg.TranslationX -= velocidade3;
		quartaimg.TranslationX -= velocidade;
	}

	void GerenciaCenario(HorizontalStackLayout HSL)
	{
		var view = (HSL.Children.First() as Image);
		if(view.WidthRequest + HSL.TranslationX < 0)
			{
				HSL.Children.Remove(view);
				HSL.Children.Add(view);
				HSL.TranslationX = view.TranslationX;
			}
	}


	async Task Desenha()
	{
		while(!estamorto)
		{
			GerenciaCenario();

			if(inimigos!= null)
				inimigos.Desenha(velocidade);

			if(!estaPulando && !estaNoAr)
			{
				AplicaGravidade();
				player.Desenha();
			}
		 else 
		 await Task.Delay(temporEntreFrames);
		}
	}
	protected override void OnAppearing()
	{
		base.OnAppearing();
		Desenha();
	}

	void Pulo(object o, TappedEventArgs a)
	{
		if(estaNoChao)
		{
			estaPulando	= true;
		}
	}
	

}