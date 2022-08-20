package com.gorvad.menuapp;

import androidx.annotation.NonNull;
import androidx.appcompat.app.AppCompatActivity;

import android.content.Intent;
import android.os.Bundle;
import android.view.Menu;
import android.view.MenuInflater;
import android.view.MenuItem;
import android.widget.Button;
import android.widget.Toast;

public class MainActivity extends AppCompatActivity
{

    @Override
    protected void onCreate(Bundle savedInstanceState)
    {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
    }

    @Override
    public boolean onCreateOptionsMenu(Menu menu)
    {
        MenuInflater menuInflater = getMenuInflater();
        menuInflater.inflate(R.menu.menu, menu);
        return true;
    }

    @Override
    public boolean onOptionsItemSelected(@NonNull MenuItem item)
    {
        Intent intent = new Intent(getApplicationContext(), SecondActivity.class);
        Intent openMapsPage = new Intent(getApplicationContext(), OpenMapsPage.class);

        switch (item.getItemId())
        {
            case R.id.googlemapspage:
                startActivity(openMapsPage);
                return true;
            case R.id.mennonite1:
                intent.putExtra("url", "google.com");
                startActivity(intent);
                return true;
            case R.id.mennonite2:
                intent.putExtra("url", "yandex.ru");
                startActivity(intent);
                return true;
            default:
                return super.onOptionsItemSelected(item);
        }
    }
}
