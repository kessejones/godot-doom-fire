using Godot;
using System;
using System.Collections.Generic;

public class Main : Node2D
{
    private IList<Fire> fires;
    const int SIZE_BLOCK = 5;
    const int WIDTH = 250;
    const int HEIGHT = 100;
    const int MAX_FIRE = 36;
    const int MIN_FIRE = 0;
    private int decay = 1;
    private float delay = .1F;
    private DynamicFont font;
    private Random random;

    private int wind = 2;

    public Main()
    {
        this.random = new Random();
        
        this.font = new DynamicFont();
        this.font.SetSize(15);
        DynamicFontData font_data = new DynamicFontData();
        font_data.SetFontPath("res://opens.ttf");
        this.font.SetFontData(font_data);

        this.fires = this.InitFires();
    }

    public override void _Ready()
    {
        this.SetProcess(true);
    }

    public override void _Process(float delta)
    {

        this.UpdateFires();
    }

    public override void _Draw()
    {
        foreach(Fire fire in this.fires)
        {
            this.DrawRect(new Rect2(fire.GetPosition(), fire.GetSize()), fire.GetColor(), true);
        }
    }

    public IList<Fire> InitFires()
    {
        IList<Fire> fires = new List<Fire>();

        int pos_x = 0;
        int pos_y = 0;
        for(int i = 0; i < HEIGHT * WIDTH; i++)
        {
            Fire fire = new Fire(new Vector2(pos_x, pos_y), new Vector2(SIZE_BLOCK, SIZE_BLOCK), 0);

            if(i >= (HEIGHT * WIDTH) - WIDTH)
                fire.SetIntensity(MAX_FIRE);
            else
                fire.SetIntensity(MIN_FIRE);

            fires.Add(fire);

            pos_x += SIZE_BLOCK;
            if((i+1) % WIDTH == 0)
            {
                pos_y += SIZE_BLOCK;
                pos_x = 0;
            }

        }

       return fires;
    }

    public void UpdateFires()
    {
        for(int i = 0; i < (HEIGHT * WIDTH) - WIDTH; i++)
        {
            Fire fire_down = this.fires[i + WIDTH];
            int rand = this.random.Next(wind);
            // int valor = fire_down.GetIntensity() - rand - decay;
            int valor = fire_down.GetIntensity() - decay - rand;
            valor = valor >= 0 ? valor : 0;
            int index = i - rand;
            if(index < 0)
                index = i;

            Fire f = this.fires[index];
            f.SetIntensity(valor);
        }

        this.Update();
    }

    private void _on_btnMinusIntensity_button_up()
    {
        if(this.decay < 36)
            this.decay++;
    }

    private void _on_btnPlusIntensity_button_up()
    {
        if(this.decay > 0)
            this.decay--;
    }
   
    private void _on_btnBoom_button_up()
    {
        foreach(Fire fire in this.fires)
        {
            fire.SetIntensity(36);
        }
    }
}
