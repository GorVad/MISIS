package com.GorVad.calculator;

import androidx.appcompat.app.AppCompatActivity;

import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.TextView;

public class MainActivity extends AppCompatActivity {

    private static final String OPERATION_TYPE_NONE = " ";
    private static final String OPERATION_TYPE_PLUS = "+";
    private static final String OPERATION_TYPE_MINUS = "-";
    private static final String OPERATION_TYPE_MUL = "*";
    private static final String OPERATION_TYPE_DIV = "/";
    private static final String OPERATION_TYPE_SIN = "sin";
    private static final String OPERATION_TYPE_COS = "cos";
    private static final String OPERATION_TYPE_PERCENT = "percent";
    private String operationType = "";

    private double result = 0;
    private double firstValue = 0;
    private double secondValue = 0;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
        Clear();

        Button C = (Button) findViewById(R.id.C);
        C.setOnClickListener(new View.OnClickListener()
        {
            @Override
            public void onClick(View v) {Clear();}
        });

        Button res = (Button) findViewById(R.id.calculate);
        res.setOnClickListener(new View.OnClickListener()
        {
            @Override
            public void onClick(View v) {Result();}
        });
    }

    public void Clear()
    {
        result = 0;
        firstValue = 0;
        secondValue = 0;
        operationType = OPERATION_TYPE_NONE;
        TextView textView = (TextView)findViewById(R.id.result);
        textView.setText(getText(R.string.text_res));
    }

    public void Result()
    {
        TextView textView = (TextView)findViewById(R.id.result);
        String strBuff = (String) textView.getText();
        try
        {
            secondValue = Double.valueOf(strBuff);
        } catch (NumberFormatException e) {secondValue = 0;}

        switch (operationType)
        {
            case OPERATION_TYPE_PLUS: result = firstValue+secondValue; break;
            case OPERATION_TYPE_MINUS: result = firstValue-secondValue; break;
            case OPERATION_TYPE_MUL: result = firstValue*secondValue; break;
            case OPERATION_TYPE_DIV: result = firstValue/secondValue; break;
            case OPERATION_TYPE_SIN: result = Math.sin(firstValue); break;
            case OPERATION_TYPE_COS: result = Math.cos(firstValue); break;
            case OPERATION_TYPE_PERCENT: result = firstValue/100; break;
            case OPERATION_TYPE_NONE: return;
            default: return;
        }
        firstValue = secondValue;
        textView.setText(Double.toString(result));
    }

    public void OnClickOperationEvent(View view)
    {
        int operation = view.getId();
        switch (operation)
        {
            case R.id.plus:operationType = OPERATION_TYPE_PLUS; break;
            case R.id.minus:operationType = OPERATION_TYPE_MINUS; break;
            case R.id.mul:operationType = OPERATION_TYPE_MUL; break;
            case R.id.div:operationType = OPERATION_TYPE_DIV; break;
            case R.id.sin:operationType = OPERATION_TYPE_SIN; break;
            case R.id.cos:operationType = OPERATION_TYPE_COS; break;
            case R.id.percent:operationType = OPERATION_TYPE_PERCENT;
            default: return;
        }

        TextView textView = (TextView)findViewById(R.id.result);
        String strBuff = (String) textView.getText();
        try
        {
            firstValue = Double.valueOf(strBuff);
        } catch (NumberFormatException e) {firstValue = 0;}
        textView.setText(getText(R.string.text_res));
    }

    public void OnClickNumberEvent(View view)
    {
        TextView textView = (TextView)findViewById(R.id.result);
        String strBuff = (String) textView.getText();
        if (strBuff.equalsIgnoreCase((String) (getText(R.string.text_res)))) strBuff = "";

        int number = view.getId();
        switch (number)
        {
            case R.id.zero: strBuff+="0"; break;
            case R.id.one: strBuff+="1"; break;
            case R.id.two: strBuff+="2"; break;
            case R.id.three: strBuff+="3"; break;
            case R.id.four: strBuff+="4"; break;
            case R.id.five: strBuff+="5"; break;
            case R.id.sin: strBuff+="6"; break;
            case R.id.seven: strBuff+="7"; break;
            case R.id.eight: strBuff+="8"; break;
            case R.id.nine: strBuff+="9"; break;
            default: return;
        }
        textView.setText(strBuff);
    }
}