package com.gorvad.test;

import android.app.Activity;
import android.view.View;
import android.widget.Button;

public class OwnCallbackImpl implements OwnCallback {
    @Override
    public void doCallback(Activity activity, View view) {
        if (view instanceof Button){
            Button b = (Button) view;
            b.setText("Был произведен вызов callback");
        }
    }
}
