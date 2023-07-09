using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.Rendering;


public class CameraEffects : MonoBehaviour
{
    public PostProcessVolume mainVolume;
    [SerializeField] private ChromaticAberration chrom;
    [SerializeField] private ColorGrading grading;
    [SerializeField] private Bloom bloom;

    private float timerGrading;
    private float timerChrom;

    [SerializeField] private float speed = 1f;

    private bool doGrading;
    private bool upGrading;
    private bool doChrome;
    private bool upChrome;
    protected virtual void Start()
    {
        mainVolume.profile.TryGetSettings(out grading);
        mainVolume.profile.TryGetSettings(out chrom);
        mainVolume.profile.TryGetSettings(out bloom);

    }
    private void Update()
    {
        AnimDo();
    }

    private void AnimDo()
    {
        OpenBr();
        CloseBr();

        OpenChrom();
        CloseChrom();
    }

    private void OpenChrom()
    {
        if (upChrome && doChrome)
        {
            timerChrom += Time.deltaTime;
            chrom.intensity.value = timerChrom;
            if (timerChrom >= 1)
            {
                chrom.intensity.value = 1;
                doChrome = false;
                StartChrome(false);
            }
        }
    }

    private void CloseChrom()
    {
        if (!upChrome && doChrome)
        {
            timerChrom -= Time.deltaTime;
            chrom.intensity.value = timerChrom;
            if (timerChrom <= 0)
            {
                chrom.intensity.value = 0;
                doChrome = false;
                chrom.active = false;
            }
        }
    }

    private void OpenBr()
    {
        if (upGrading && doGrading)
        {
            timerGrading += Time.deltaTime * 50 * speed;
            grading.brightness.value = timerGrading;
            if (timerGrading >= 15)
            {
                grading.brightness.value = 15;
                doGrading = false;
            }
        }
    }
    private void CloseBr()
    {
        if (!upGrading && doGrading)
        {
            timerGrading -= Time.deltaTime * 70 * speed;
            grading.brightness.value = timerGrading;
            if (timerGrading <= -95)
            {
                grading.brightness.value = -100;
                doGrading = false;
            }
        }
    }

    public void StartAnim(bool flag)
    {
        doGrading = true;
        upGrading = flag;
        if (upGrading) timerGrading = -100;
        else timerGrading = 15;
    }
    public void StartChrome(bool flag)
    {
        doChrome = true;
        upChrome = flag;
        if (upChrome)
        {
            timerChrom = 0;
            chrom.active = true;
        }
        else timerGrading = 15;
    }

    public void doColor()
    {
        doGrading = true;
        upGrading = false;
        timerGrading = 15;
    }
    public void DoChrome()
    {
        doChrome = true;
        upChrome = true;
        timerChrom = 0;
        chrom.active = true;
    }

    public void SetBloom(bool flag)
    {
        bloom.active = flag;
    }
}
