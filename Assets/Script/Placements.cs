using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

public class Placements
{
    private List<PlacementInfo> placementList;
    private int[] randomIndices;
    System.Random r = new System.Random(1);

    public Placements()
    {
        this.placementList = new List<PlacementInfo>();
        InitializeBoard();
        InitializeKomadai();
        InitializeRandomIndices();
    }

    /// <summary>
    /// </summary>
    /// <param name="komanumMax"></param>
    /// <param name="komaList"></param>
    public void MakePlaceRandomly(List<GameObject>  komaList)
    {
        ShuffleIndices();
        var komaNum = komaList.Count();
        var copiedRandomindices = new int[komaNum];
        Array.Copy(this.randomIndices, 0, copiedRandomindices, 0, komaNum);

        var blackCapturedTable = new Dictionary<int, int>();
        var whiteCapturedTable = new Dictionary<int, int>();

        for (int komaIdx = 0; komaIdx < komaNum; komaIdx++)
        {
            var randomIdx = copiedRandomindices[komaIdx];
            if (randomIdx < 81)
            {
                komaList[komaIdx].transform.position = this.placementList[randomIdx].position;
                komaList[komaIdx].transform.rotation = this.placementList[randomIdx].rotation;
            }
            else if (randomIdx < 88)
            {
                blackCapturedTable.Add(komaIdx, randomIdx);
            }
            else
            {
                whiteCapturedTable.Add(komaIdx, randomIdx);
            }
        }

        SetCaptured(blackCapturedTable.Values.ToList(), "Black");
        SetCaptured(whiteCapturedTable.Values.ToList(), "White");

        foreach (KeyValuePair<int, int> item in blackCapturedTable)
        {
            komaList[item.Key].transform.position = this.placementList[item.Value].position;
            komaList[item.Key].transform.rotation = this.placementList[item.Value].rotation;
        }

        foreach (KeyValuePair<int, int> item in whiteCapturedTable)
        {
            komaList[item.Key].transform.position = this.placementList[item.Value].position;
            komaList[item.Key].transform.rotation = this.placementList[item.Value].rotation;
        }
    }

    public void Reset()
    {
        foreach (PlacementInfo plInfo in this.placementList)
        {
            plInfo.Reset();
        }
    }

    private void SetCaptured(List<int> capturedIdxList, string turn)
    {
        var capturedCount = capturedIdxList.Count();
        var single_location = GetRandomLocations("All")[0];
        var double_locations = GetRandomLocations("UpDown");
        int rand = UnityEngine.Random.Range(0, 2);

        if(capturedCount == 0)
        {
            return;
        }

        if (capturedCount < 5)
        {
            SetCapturedVecAndQtn(single_location, capturedIdxList, turn);
            return;
        }

        switch (capturedCount)
        {
            case 5:
                if(rand == 0)
                {
                    SetCapturedVecAndQtn(double_locations[0], capturedIdxList.GetRange(0, 1), turn);
                    SetCapturedVecAndQtn(double_locations[1], capturedIdxList.GetRange(1, 4), turn);
                }
                else
                {
                    SetCapturedVecAndQtn(double_locations[0], capturedIdxList.GetRange(0, 2), turn);
                    SetCapturedVecAndQtn(double_locations[1], capturedIdxList.GetRange(2, 3), turn);
                }
                break;
            case 6:
                if (rand == 0)
                {
                    SetCapturedVecAndQtn(double_locations[0], capturedIdxList.GetRange(0, 2), turn);
                    SetCapturedVecAndQtn(double_locations[1], capturedIdxList.GetRange(2, 4), turn);
                }
                else
                {
                    SetCapturedVecAndQtn(double_locations[0], capturedIdxList.GetRange(0, 3), turn);
                    SetCapturedVecAndQtn(double_locations[1], capturedIdxList.GetRange(3, 3), turn);
                }
                break;
            case 7:
                SetCapturedVecAndQtn(double_locations[0], capturedIdxList.GetRange(0, 3), turn);
                SetCapturedVecAndQtn(double_locations[1], capturedIdxList.GetRange(3, 4), turn);
                break;
        }
    }
    
    private string[] GetRandomLocations(string mode)
    {
        var baseLocations = new string[] {"up", "down", "middle" };
        if(mode == "UpDown")
        {
            int randomVal = UnityEngine.Random.Range(0, 2);
            return new string[] { baseLocations[randomVal], baseLocations[-(randomVal - 1)], baseLocations[2] };
        }
        else if(mode == "All")
        {
            return baseLocations.OrderBy(i => Guid.NewGuid()).ToArray();
        }
        else
        {
            throw new ArgumentException();
        }
    }

    private float GetZByLocation(string location, string turn)
    {
        if (location == "up")
        {
            return (turn == "Black") ? UnityEngine.Random.Range(-0.2f, 0.6f) : UnityEngine.Random.Range(-6.4f, -5.8f);
        }
        else if (location == "middle")
        {
            return (turn == "Black") ? UnityEngine.Random.Range(0.5f, 1.5f) : UnityEngine.Random.Range(-7.6f, -6.3f);
        }
        else if (location == "down")
        {
            return (turn == "Black") ? UnityEngine.Random.Range(1.6f, 2.4f) : UnityEngine.Random.Range(-8.4f, -7.5f);
        }
        else
        {
            throw new ArgumentException();
        }
    }

    private float GetXByCapturedCount(int capturedCount, string turn)
    {
        if (capturedCount == 1)
        {
            return (turn == "Black") ? UnityEngine.Random.Range(9.2f, 12.0f) : UnityEngine.Random.Range(26.9f, 29.9f);
        }
        else if (capturedCount == 2)
        {
            return (turn == "Black") ? UnityEngine.Random.Range(10.5f, 12.0f): UnityEngine.Random.Range(26.9f, 29.2f);
        }
        else if (capturedCount == 3)
        {
            return (turn == "Black") ? UnityEngine.Random.Range(11.2f, 12.0f) : UnityEngine.Random.Range(26.9f, 28.5f);
        }
        else if (capturedCount == 4)
        {
            return (turn == "Black") ? UnityEngine.Random.Range(11.5f, 12.0f) : UnityEngine.Random.Range(26.9f, 27.8f);
        }
        else
        {
            throw new ArgumentException();
        }
    }

    private float GetRotationYByCapturedCount(int capturedCount)
    {
        if (capturedCount == 1)
        {
            return UnityEngine.Random.Range(-5.0f, 5.0f);
        }
        else if (capturedCount == 2)
        {
            return UnityEngine.Random.Range(-2.0f, 8.0f);
        }
        else if (capturedCount == 3)
        {
            return UnityEngine.Random.Range(11.0f, 17.0f);
        }
        else if (capturedCount == 4)
        {
            return UnityEngine.Random.Range(14.0f, 21.0f);
        }
        else
        {
            throw new ArgumentException();
        }
    }

    private void SetCapturedVecAndQtn(string location, List<int> capturedIdxList, string turn)
    {
        var rad18 = Mathf.Deg2Rad * 18.0f;

        var x = GetXByCapturedCount(capturedIdxList.Count(), turn);
        var y = 1.0f;
        var z = GetZByLocation(location, turn);

        var rotationX = 0;
        var rotationY = GetRotationYByCapturedCount(capturedIdxList.Count());

        var radY = Mathf.Deg2Rad * rotationY;

        float centerX;
        float centerZ;
        if(turn == "Black")
        {
            centerX = x - 2.2f * Mathf.Sin(radY);
            centerZ = z - 2.2f * Mathf.Cos(radY);
        }
        else
        {
            centerX = x + 2.2f * Mathf.Sin(radY);
            centerZ = z + 2.2f * Mathf.Cos(radY);
        }

        var vec = new Vector3(x, y + 0.3f, z);
        var conpensationRotationY = (turn == "Black") ? 180 : 0;
        var qtn = Quaternion.Euler(rotationX + 180, rotationY + conpensationRotationY, 0);
        foreach(int randomIdx in capturedIdxList)
        {
            this.placementList[randomIdx].position = vec;
            this.placementList[randomIdx].rotation = qtn;
            var newX = (x - centerX) * Mathf.Cos(rad18) - (z - centerZ) * Mathf.Sin(rad18) + centerX;
            var newZ = (x - centerX) * Mathf.Sin(rad18) + (z - centerZ) * Mathf.Cos(rad18) + centerZ;
            x = newX;
            z = newZ;
            rotationY -= 18.0f;
            vec = new Vector3(x, y + 0.3f, z);
            qtn = Quaternion.Euler(rotationX + 180, rotationY + conpensationRotationY, 0);
        }
    }

    private void InitializeRandomIndices()
    {
        this.randomIndices = new int[95];
        for (int komaIdx = 0; komaIdx < 95; komaIdx++)
        {
            randomIndices[komaIdx] = komaIdx;
        }
    }

    private void ShuffleIndices()
    {
        this.randomIndices = this.randomIndices.OrderBy(i => Guid.NewGuid()).ToArray();
    }

    private void InitializeBoard()
    {
        for (int suji = 1; suji < 10; suji++)
        {
            for (int dan = 1; dan < 10; dan++)
            {
                var turn = GetTurnRandomly();
                var placementInfo = new PlacementInfo(suji, dan);
                placementList.Add(placementInfo);
            }
        }
    }

    private void InitializeKomadai()
    {
        foreach(string turn in new List<string> {"Black","White" })
        {
            for(int i=0; i < 8; i++)
            {
                var placementInfo = new PlacementInfo(turn);
                placementList.Add(placementInfo);
            }
        }
    }

    private string GetTurnRandomly()
    {
        int randomVal = r.Next(2);
        return (randomVal == 0) ? "Black" : "White";
    }
}
