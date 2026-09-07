using Mox.NDisplay;
using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class PharusVisualizer : MonoBehaviour
{
    [SerializeField]
    private GameObject _pharusTrackPrefab = null;

    Dictionary<int, GameObject> _pharusTrackVisuals = new Dictionary<int, GameObject>();

    private bool _offsetSet = false;

    void Start()
    {
        if(CameraHandler.Instance.FrontLeftCornerSet == true)
        {
            transform.localPosition = new Vector3(CameraHandler.Instance.FrontLeftCorner.x, transform.localPosition.y, CameraHandler.Instance.FrontLeftCorner.y);

            _offsetSet = true;
        }
    }

    void Update()
    {
        if (_offsetSet == false
            && CameraHandler.Instance.FrontLeftCornerSet == true)
        {
            transform.localPosition = new Vector3(CameraHandler.Instance.FrontLeftCorner.x, transform.localPosition.y, CameraHandler.Instance.FrontLeftCorner.y);

            _offsetSet = true;
        }

        UpdatePharusTracks();
    }

    private void UpdatePharusTracks()
    {
        if (_pharusTrackPrefab != null)
        {
            List<Mox.Sync.Tracking.MoxPharusTrack> tracks = Mox.Sync.Tracking.PharusTracks();

            List<int> trackIds = new List<int>();

            for (int i = 0; i < tracks.Count; i++)
            {
                int id = tracks[i].Id;

                Vector2 nPos = Mox.Sync.Tracking.NormalizePharusTrack(tracks[i]);

                // Debug.Log("norm pos: " + nPos);

                trackIds.Add(id);

                if (_pharusTrackVisuals.ContainsKey(id) == false)
                {
                    GameObject newTrack = GameObject.Instantiate(_pharusTrackPrefab);
                    newTrack.transform.SetParent(gameObject.transform, false);
                    _pharusTrackVisuals.Add(id, newTrack);
                }

                _pharusTrackVisuals[id].transform.localPosition = tracks[i].Position;
            }

            List<int> visualsToDelete = new List<int>();

            foreach (KeyValuePair<int, GameObject> dTrack in _pharusTrackVisuals)
            {
                if (trackIds.Contains(dTrack.Key) == false)
                {
                    visualsToDelete.Add(dTrack.Key);
                }
            }

            foreach (int key in visualsToDelete)
            {
                GameObject.Destroy(_pharusTrackVisuals[key]);
                _pharusTrackVisuals.Remove(key);
            }
        }
    }
}
