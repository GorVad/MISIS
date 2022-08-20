package com.gorvad.menuapp;

import android.content.Intent;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.TextView;

import androidx.appcompat.app.AppCompatActivity;

public class OpenMapsPage extends AppCompatActivity
{
    Button btn;

    @Override
    public void onCreate(Bundle savedInstanceState)
    {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.mapcoords);

        btn = (Button) findViewById(R.id.openMaps);
        btn.setOnClickListener(oclBtn);
    }

    View.OnClickListener oclBtn = new View.OnClickListener()
    {
        @Override
        public void onClick(View v)
        {
            Intent intent = new Intent(getApplicationContext(), SecondActivity.class);

            intent.putExtra("url", "https://www.google.ru/maps/@"+(TextView) findViewById(R.id.longitude)+","+(TextView) findViewById(R.id.latitude)+"z");
            startActivity(intent);
        }
    };
}
